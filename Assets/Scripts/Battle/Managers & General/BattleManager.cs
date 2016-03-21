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

    public BattleDisplayManager battleDisplayManager;
    public DebugDisplayManager debugDisplayManager;
    public EnemyManager enemyManager;
    
    public float battleTimer = 0.0f;
    public static System.Random randomer;

    //Hero variables

    public Hero heroObjectOne;
    public Hero heroObjectTwo;

    public Hero selectedHero;
    public Hero queuedHero;

    public Ability targetingAbility;


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

        InvokeRepeating("ApplyHealthAndManaRegen", 0, 0.2f);
        
        battleDisplayManager.UpdateHealthText();
        battleDisplayManager.NoHeroSelected();
        battleDisplayManager.InitNameText();
        
        debugDisplayManager.InitDebugText();
        debugDisplayManager.UpdateDebugText();

    } //end Start()


    // Update is called once per frame. It does everything.
    void Update() {

        //Every-frame maintenance

        battleDisplayManager.CheckForEnemyHealthRemoval();
        battleDisplayManager.UpdateHealthText();
        battleDisplayManager.UpdateManaText();
        battleDisplayManager.UpdateSelectedHeroText();
        debugDisplayManager.UpdateDebugText();
        battleTimer += Time.deltaTime;

        //Checking for input

        CheckForHeroSelectionInput();

        if(selectedHero != null) {
            if (selectedHero.canTakeCommands) {
                CheckForAbilitySelectionInput();
            }
        } //end if hero is selected

        
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


    //BattleState switch. Not actually a switch, but this  decides what a hero does in every frame based on their battlestate.
        //Some notes - Target only applies if a target is being chosen. 
        //Abilities that don't need player input for a target skip the targeting BattleState, and call the appropriate function on TargetManager by themselves.
        //At some point, BattleState.Burst might work this way too. For now, it's fine.

    void CheckBattleState(Hero hero) {

        if ((hero.currentBattleState == Hero.BattleState.Wait) 
            | (hero.currentBattleState == Hero.BattleState.InfCharge) 
            | (hero.currentBattleState == Hero.BattleState.Dead)) {
            return;
        }

        else if (hero.currentBattleState == Hero.BattleState.Target) {
            if (Input.GetMouseButtonDown(0)) {
                selectedHero.currentAbility.CastRay();
            }
        }

        else if(hero.currentBattleState == Hero.BattleState.Charge) {
            hero.currentAbility.CheckCharge();
        }

        else if (hero.currentBattleState == Hero.BattleState.InfBarrage) {
            hero.currentAbility.AbilityMap();
        }

        else if((hero.currentBattleState == Hero.BattleState.Ability) | (hero.currentBattleState == Hero.BattleState.InfBarrage)) {
            hero.currentAbility.AbilityMap();
        }

    } //end CheckBattleState()

    

//Input checking functions

    void CheckForHeroSelectionInput() {

        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            selectedHero = heroObjectOne;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            selectedHero = heroObjectTwo;
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            CancelAbility(selectedHero);
        }

    } //end CheckForHeroSelectionInput()


    void CheckForAbilitySelectionInput() {

        Ability abilityToApply = null;

        if ((Input.GetKeyDown(KeyCode.Q)) && (selectedHero.abilityOne.cooldownEndTimer <= Time.time)) {
            abilityToApply = selectedHero.abilityOne;
        }
        else if ((Input.GetKeyDown(KeyCode.W)) && (selectedHero.abilityTwo.cooldownEndTimer <= Time.time)) {
            abilityToApply = selectedHero.abilityTwo;
        }
        else if ((Input.GetKeyDown(KeyCode.E)) && (selectedHero.abilityThree.cooldownEndTimer <= Time.time)) {
            abilityToApply = selectedHero.abilityThree;
        }
        else if ((Input.GetKeyDown(KeyCode.A)) && (selectedHero.abilityFour.cooldownEndTimer <= Time.time)) {
            abilityToApply = selectedHero.abilityFour;
        }
        else if ((Input.GetKeyDown(KeyCode.S)) && (selectedHero.abilityFive.cooldownEndTimer <= Time.time)) {
            abilityToApply = selectedHero.abilityFive;
        }
        else if ((Input.GetKeyDown(KeyCode.D)) && (selectedHero.abilitySix.cooldownEndTimer <= Time.time)) {
            abilityToApply = selectedHero.abilitySix;
        }
        

        if (abilityToApply != null) {
            if (selectedHero.currentBattleState != Hero.BattleState.InfCharge) {
                selectedHero.currentAbility = abilityToApply;
                abilityToApply.InitAbility();
            }
            else if (abilityToApply == selectedHero.currentAbility) {
                selectedHero.currentAbility.targetingManager.SortTargetingType(selectedHero.currentAbility);
            }
        }
        
 
    } //end CheckForAbilitySelectionInput()
    
    
//Ability handling functions

        
    public void CancelAbility(Hero hero) {
        hero.selectedAbility = null;
        hero.targetingAbility = null;
        if ((hero.currentBattleState != Hero.BattleState.Charge) && (hero.currentBattleState != Hero.BattleState.InfCharge) && (hero.currentAbility != null)) {
            hero.currentAbility.cooldownEndTimer = Time.time + hero.currentAbility.cooldownDuration;
        }
        hero.currentAbility = null;
        hero.canTakeCommands = true;
        hero.currentBattleState = Hero.BattleState.Wait;
    }


    public void SelectedToTargeting(Hero hero) {

        hero.targetingAbility = hero.selectedAbility;
        hero.selectedAbility = null;
    }

    public void TargetingToCurrent(Hero hero) {

        hero.currentAbility = hero.targetingAbility;
        hero.targetingAbility = null;
    }

    public void SelectedToCurrent(Hero hero) {

        hero.currentAbility = hero.selectedAbility;
        hero.selectedAbility = null;
    }


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
        
        ResetHeroHealth();
        ResetHeroMana();
        foreach(Hero hero in heroList) {
            hero.currentBattleState = Hero.BattleState.Wait;
            hero.selectedAbility = null;
            hero.currentAbility = null;
            hero.targetingAbility = null;
        }
    } //end ResetHeroes()


    public void ResetHeroHealth() {
        foreach(Hero hero in heroList) {
            hero.currentHealth = hero.maxHealth;
        }
    } //endResetHeroHealth()
    

    public void ResetHeroMana() {
        foreach (Hero hero in heroList) {
            hero.currentMana = hero.maxMana;
        }
    } //endResetHeroMana()


    public void ResetEnemyHealths() {
        foreach(Enemy enemy in enemyList) {
            enemy.currentHealth = enemy.maxHealth;
        } //end foreach
    } //endResetEnemyHealth()




//Other functions




    void ApplyHealthAndManaRegen() {

        foreach (Enemy enemy in enemyList) {
            if (enemy.currentHealth < enemy.maxHealth) {
                enemy.currentHealth += (enemy.healthRegen / 5);
            }
        } //end foreach enemy

        foreach (Hero hero in heroList) {
            if(hero.currentHealth < hero.maxHealth) {
                hero.currentHealth += (hero.healthRegen / 5);
            }
            if (hero.currentMana < hero.maxMana) {
                hero.currentMana += (hero.manaRegen / 5);
            }
        } //end foreach hero

    } //end ApplyHealthRegen()


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
        SceneManager.LoadScene(sceneName: "Overworld");
    }

    void BattleLost() {
        SceneManager.LoadScene(sceneName: "Overworld");
    }



//Testing functions

    void DebugAllHeroDamage() {

        foreach(Hero hero in heroList) {
            hero.currentHealth -= 120;
            hero.SpawnDamageText(120);
        }
    }

    void DebugSelectedHeroDamage () {

        if(selectedHero != null) {
            selectedHero.currentHealth -= 177;
            selectedHero.SpawnDamageText(177);
        }
    }

} //end BattleManager


    
