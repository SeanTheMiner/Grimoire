using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

using Heroes;
using Enemies;
using Abilities;
using Biomes;

public class BattleManager : MonoBehaviour {

    public BattleDisplayManager battleDisplayManager = new BattleDisplayManager();
    public DebugDisplayManager debugDisplayManager = new DebugDisplayManager();

    public float battleTimer = 0.0f;

    //Hero variables

    public Hero heroObjectOne;
    public Hero heroObjectTwo;

    public Hero selectedHero;
    public Hero queuedHero;

    public Ability targetingAbility;
    private bool abilityTargeterActive;

    public Enemy enemyObjectOne;
    public Enemy enemyObjectTwo;
    public Enemy enemyObjectThree;

    public List<Hero> heroList = new List<Hero>();
    public List<Enemy> enemyList = new List<Enemy>();
   
    public GameObject[] allHeroes;
    public GameObject[] allEnemies;
    public GameObject[] allHeroesAndEnemies;



    //LET'S GET IT STARTED IN HERE
    void Start() {

        PopulateHeroList();
        PopulateEnemyList();

        ResetHeroes();
        ResetEnemyHealths();
        
        battleDisplayManager.UpdateHealthText();
        battleDisplayManager.NoHeroSelected();
        battleDisplayManager.InitNameText();

        debugDisplayManager.InitDebugText();
        debugDisplayManager.UpdateDebugText();

        abilityTargeterActive = false;

    } //end Start()


    // Update is called once per frame. It does everything.
    void Update() {

        //Every-frame maintenance

        battleDisplayManager.UpdateHealthText();
        debugDisplayManager.UpdateDebugText();
        battleTimer += Time.deltaTime;

        //Checking for input

        //Unless we're targeting an abilty, check for hero selection input
        if(abilityTargeterActive == false) {
            CheckForHeroSelectionInput();
        }

        //Unless there's no hero selected, check for ability selection input
        if(selectedHero != null) {
            CheckForAbilitySelectionInput();
        }

        //If we're targeting an ability, and the mouse is clicked, cast a selecter ray
        if ((abilityTargeterActive) && (Input.GetMouseButtonDown(0))) {
            CastSelecterRay();
        }


        //Hero BattleState switch
            //Figure out what each Hero is currently doing, and see if it needs to change

        foreach (Hero hero in heroList) {
            CheckBattleState(hero);
        }
       
        foreach (Enemy enemy in enemyList) {
            CheckIfEnemyIsDead(enemy);
        }

        if (enemyList.Count <= 0) {
            BattleWon();
        }

    } //end Update


    //BattleState switch, since I'll be working on this constantly

    void CheckBattleState(Hero hero) {

        if (hero.currentBattleState == Hero.BattleState.Wait) {
            return;
        }

        if(hero.currentBattleState == Hero.BattleState.Charge) {
            hero.currentAbility.CheckCharge();
        }

        if(hero.currentBattleState == Hero.BattleState.Burst) {
            hero.currentAbility.AbilityMap();
        }

        if(hero.currentBattleState == Hero.BattleState.Barrage) {
            hero.currentAbility.AbilityMap();
        }

        if(hero.currentBattleState == Hero.BattleState.Dead) {
            return;
        }

    } //end CheckBattleState()




    //Initializing functions

    public void PopulateHeroList() {
        allHeroes = GameObject.FindGameObjectsWithTag("Hero");
        foreach(GameObject heroObject in allHeroes) {
            if(heroObject.activeInHierarchy) {
                heroList.Add(heroObject.GetComponent<Hero>());
            } //end if active in hierarchy
        } //end foreach
    } //end PopulateHeroList()


    public void PopulateEnemyList() {
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemyObject in allEnemies) {
            if(enemyObject.activeInHierarchy) {
                enemyList.Add(enemyObject.GetComponent<Enemy>());
            }//end if active in hierarchy
        } //end foreach
    } //end PopulateEnemyList()


    public void ResetHeroes() {
        ResetHeroHealths();
        foreach(Hero hero in heroList) {
            hero.currentBattleState = Hero.BattleState.Wait;
            hero.currentAbility = null;
            hero.targetingAbility = null;
        }
    }

    public void ResetHeroHealths() {
        foreach(Hero hero in heroList) {
            hero.currentHealth = hero.maxHealth;
        } //end foreach
    } //endResetHeroHealth()


    public void ResetEnemyHealths() {
        foreach(Enemy enemy in enemyList) {
            enemy.currentHealth = enemy.maxHealth;
        } //end foreach
    } //endResetEnemyHealth()

    
    //Input checking functions

    void CheckForHeroSelectionInput() {

        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            selectedHero = heroObjectOne;
            battleDisplayManager.UpdateSelectedHeroText();
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            selectedHero = heroObjectTwo;
            battleDisplayManager.UpdateSelectedHeroText();
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            selectedHero = null;
            battleDisplayManager.NoHeroSelected();
        }
    } //end CheckForHeroSelectionInput()


    void CheckForAbilitySelectionInput() {

        if(Input.GetButtonDown("Ability One")) {
            if(selectedHero.abilityOne.requiresTarget == false) {
                selectedHero.currentAbility = selectedHero.abilityOne;
                ExecuteAbility();
            }
            else {
                abilityTargeterActive = true;
                selectedHero.targetingAbility = selectedHero.abilityOne;
            }
          
        } //end if Ability One

        if(Input.GetButtonDown("Ability Two")) {
            if(selectedHero.abilityTwo.requiresTarget == false) {
                selectedHero.currentAbility = selectedHero.abilityTwo;
                ExecuteAbility();
            }
            else {
                abilityTargeterActive = true;
                selectedHero.targetingAbility = selectedHero.abilityTwo;
            }

        } //end if Ability One

    } //end CheckForHeroSelectionInput()


    //Other functions

    void CastSelecterRay() {

        RaycastHit objectHit;
        Ray selectingRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(selectingRay, out objectHit)) {

            if((objectHit.collider.tag == "Enemy") && (selectedHero.targetingAbility.targetScope == Ability.TargetScope.SingleEnemy)) {
                selectedHero.currentAbility = selectedHero.targetingAbility;
                selectedHero.targetingAbility = null;
                selectedHero.currentAbility.targetEnemy = objectHit.collider.gameObject.GetComponent<Enemy>();
                ExecuteAbility();
            } //end if enemy & enemytargetable

            else if((objectHit.collider.tag == "Hero") && (selectedHero.currentAbility.targetScope == Ability.TargetScope.SingleHero)) {
                selectedHero.currentAbility = selectedHero.targetingAbility;
                selectedHero.targetingAbility = null;
                selectedHero.currentAbility.targetHero = objectHit.collider.gameObject.GetComponent<Hero>();
                ExecuteAbility();
            } //end if hero & herotargetable

            abilityTargeterActive = false;

        }  //end if objectHit

    } //end CastSelecterRay


    void ExecuteAbility() {
        if (selectedHero.currentAbility.requiresCharge) {
            selectedHero.currentAbility.InitCharge();
        }
        else {
            selectedHero.currentAbility.InitAbility();
        }
    }


    void CheckIfEnemyIsDead(Enemy enemy) {
        if(enemy.currentHealth <= 0) {
            RemoveEnemy(enemy);
        }
    }

    void RemoveEnemy(Enemy enemy) {
        enemy.currentHealth = 0;
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
        Destroy(enemy);
    }

    void CheckIfHeroIsDead(Hero hero) {
        if(hero.currentHealth <= 0) {
            hero.currentHealth = 0;
            hero.currentBattleState = Hero.BattleState.Dead;
        }
    }

    void BattleWon() {
        Debug.Log("Battlewoncalled");
        SceneManager.LoadScene(sceneName: "Overworld");
    }

    void BattleLost() {
        SceneManager.LoadScene(sceneName: "Overworld");
    }
}


    
