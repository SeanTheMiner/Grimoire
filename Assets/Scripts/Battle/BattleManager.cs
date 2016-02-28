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
    public TargetingManager targetingManager;

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

    public List<Hero> heroList;
    public List<Enemy> enemyList;
   
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

    } //end Start()


    // Update is called once per frame. It does everything.
    void Update() {

        //Every-frame maintenance
        
        battleDisplayManager.UpdateHealthText();
        battleDisplayManager.UpdateSelectedHeroText();
        debugDisplayManager.UpdateDebugText();
        battleTimer += Time.deltaTime;

        //Checking for input

        
        CheckForHeroSelectionInput();
      

        //Unless there's no hero selected, or the selected hero is charging an ability, check for ability selection input
        if((selectedHero != null) && (selectedHero.canTakeCommands)) {
            CheckForAbilitySelectionInput();
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
           if(Input.GetMouseButtonDown(0)) {
                targetingManager.CastSelecterRay(hero);
            }
        }

        if(hero.currentBattleState == Hero.BattleState.ReTarget) {
            targetingManager.ReTargetRandomEnemy(hero); 
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

        if((Input.GetButtonDown("Ability One")) && (selectedHero.abilityOne.cooldownEndTimer <= Time.time)) {
            selectedHero.selectedAbility = selectedHero.abilityOne;
            ExecuteAbilitySelection(selectedHero);
        }

        if((Input.GetButtonDown("Ability Two")) && (selectedHero.abilityTwo.cooldownEndTimer <= Time.time)) {
            selectedHero.selectedAbility = selectedHero.abilityTwo;
            ExecuteAbilitySelection(selectedHero);
        }

    } //end CheckForHeroSelectionInput()

    void ExecuteAbilitySelection(Hero hero) {

        if(hero.selectedAbility.requiresTargeting) {
            SelectedToTargeting(hero);
            hero.currentBattleState = Hero.BattleState.Target;
        }
        else {
            SelectedToTargeting(hero);
            targetingManager.SortUntargetedType(hero);
        }

    } //end ExecuteAbilitySelection()



    public void ExecuteAbility(Hero hero) {

        if(hero.currentAbility.requiresCharge) {
            hero.currentAbility.InitCharge();
        }
        else {
            hero.currentAbility.InitAbility();
        }

    } //end ExecuteAbility()

    public void CancelAbility(Hero hero) {
        hero.selectedAbility = null;
        hero.targetingAbility = null;
        if ((hero.currentBattleState != Hero.BattleState.Charge) && (hero.currentAbility != null)) {
            hero.currentAbility.cooldownEndTimer = Time.time + hero.currentAbility.cooldownDuration;
        }
        hero.currentAbility = null;
        hero.canTakeCommands = true;
        hero.currentBattleState = Hero.BattleState.Wait;
    }


    //Ability shuffling functions

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


    //Other functions

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
            hero.currentHealth -= 100;
        }
    }

    void DebugSelectedHeroDamage () {

        if(selectedHero != null) {
            selectedHero.currentHealth -= 100;
        }
    }
    
} //end BattleManager


    
