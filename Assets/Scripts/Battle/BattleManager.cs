using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
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
    static System.Random randomer = new System.Random();

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
        battleDisplayManager.UpdateSelectedHeroText();
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

        //DEBUG FUNCTIONS

        if(Input.GetKeyDown(KeyCode.Z)) {
            DebugAllHeroDamage();
        }

        if(Input.GetKeyDown(KeyCode.X)) {
            DebugSelectedHeroDamage();
        }

    } //end Update


    //BattleState switch, since I'll be working on this constantly

    void CheckBattleState(Hero hero) {

        if (hero.currentBattleState == Hero.BattleState.Wait) {
            return;
        }

        if(hero.currentBattleState == Hero.BattleState.Target) {
            if (hero.targetingAbility.targetScope == Ability.TargetScope.Untargeted) {
                TargetRandomEnemy(hero);
            }
            if (hero.targetingAbility.targetScope == Ability.TargetScope.AllEnemies) {
                TargetAllEnemies(hero);
            }
            if (hero.targetingAbility.targetScope == Ability.TargetScope.AllHeroes) {
                TargetAllHeroes(hero);
            }
            //When you finally get around to making a TargetingManager, this should just call TargetingManager.Main(), 
                //which can shoot off to diff functions depending on the targetScope.
        }

        if(hero.currentBattleState == Hero.BattleState.ReTarget) {
            ReTargetRandomEnemy(hero); 
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
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            selectedHero = heroObjectTwo;
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            selectedHero = null;
        }

    } //end CheckForHeroSelectionInput()


    void CheckForAbilitySelectionInput() {

        if((Input.GetButtonDown("Ability One")) && (selectedHero.abilityOne.cooldownEndTimer <= Time.time)) {
            if(selectedHero.abilityOne.targetScope == Ability.TargetScope.Untargeted) {
                TargetRandomEnemy(selectedHero);
            }
            else {
                abilityTargeterActive = true;
                selectedHero.targetingAbility = selectedHero.abilityOne;
                selectedHero.currentBattleState = Hero.BattleState.Target;
            }
          
        } //end if Ability One

        if((Input.GetButtonDown("Ability Two")) && (selectedHero.abilityTwo.cooldownEndTimer <= Time.time)) {
            if(selectedHero.abilityTwo.targetScope == Ability.TargetScope.Untargeted) {
                TargetRandomEnemy(selectedHero);
            }
            else {
                abilityTargeterActive = true;
                selectedHero.targetingAbility = selectedHero.abilityTwo;
                selectedHero.currentBattleState = Hero.BattleState.Target;
            }

        } //end if Ability Two

    } //end CheckForHeroSelectionInput()

    void ExecuteAbilitySelection(Ability ability) {
        
        //It seems like the redundancy up in CheckForAbilitySelectionInput() can be cleared up here,
            //but passing in (hero, ability) doesn't really work, and selectedHero doesn't take just any old ability, as it turns out.
            //This would be best if you could get it working, but it's really just ugly at the moment, not non-functional.

    }

    //Functions that should probably go on a targeter

    void CastSelecterRay() {

        RaycastHit objectHit;
        Ray selectingRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(selectingRay, out objectHit)) {

            if((objectHit.collider.tag == "Enemy") && (selectedHero.targetingAbility.targetScope == Ability.TargetScope.SingleEnemy)) {
                selectedHero.currentAbility = selectedHero.targetingAbility;
                selectedHero.targetingAbility = null;
                selectedHero.currentAbility.targetEnemy = objectHit.collider.gameObject.GetComponent<Enemy>();
                ExecuteAbility(selectedHero);
            } //end if enemy & enemytargetable

            else if((objectHit.collider.tag == "Hero") && (selectedHero.currentAbility.targetScope == Ability.TargetScope.SingleHero)) {
                selectedHero.currentAbility = selectedHero.targetingAbility;
                selectedHero.targetingAbility = null;
                selectedHero.currentAbility.targetHero = objectHit.collider.gameObject.GetComponent<Hero>();
                ExecuteAbility(selectedHero);
            } //end if hero & herotargetable

            abilityTargeterActive = false;

        }  //end if objectHit

    } //end CastSelecterRay

    void TargetToCurrent(Hero hero) {
        hero.currentAbility = hero.targetingAbility;
        hero.targetingAbility = null;
    }

    void TargetAllEnemies(Hero hero) {
        TargetToCurrent(hero);
        foreach(Enemy enemyTarget in enemyList) {
            hero.currentAbility.targetEnemyList.Add(enemyTarget);
        }
        ExecuteAbility(hero);
    }

    void TargetAllHeroes(Hero hero) {
        TargetToCurrent(hero);
        foreach(Hero heroTarget in heroList) {
            hero.currentAbility.targetHeroList.Add(heroTarget);
        }
        ExecuteAbility(hero);
    }

    void TargetRandomEnemy(Hero hero) {
        TargetToCurrent(hero);
        hero.currentAbility.targetEnemy = enemyList[randomer.Next(enemyList.Count)];
        ExecuteAbility(hero);
    }


    void ReTargetRandomEnemy(Hero hero) {
        hero.currentAbility.targetEnemy = enemyList[randomer.Next(enemyList.Count)];
        hero.currentAbility.SetBattleState();
    }


    //Other functions

    void ExecuteAbility(Hero hero) {
        if (hero.currentAbility.requiresCharge) {
            hero.currentAbility.InitCharge();
        }
        else {
            hero.currentAbility.InitAbility();
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

    void DebugAllHeroDamage() {
        foreach(Hero hero in heroList) {
            hero.currentHealth -= 100;
        }
    }

    void DebugSelectedHeroDamage () {
        if(selectedHero != null) {
            selectedHero.currentHealth -= 100;
        }
    }
    
} //end BattleManager


    
