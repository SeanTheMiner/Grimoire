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

        battleDisplayManager.CheckForEnemyHealthRemoval();
        battleDisplayManager.UpdateHealthText();
        battleDisplayManager.UpdateSelectedHeroText();
        debugDisplayManager.UpdateDebugText();
        battleTimer += Time.deltaTime;

        //Checking for input

        CheckForHeroSelectionInput();

        if(selectedHero != null) {
            if ((selectedHero.selectedAbility != null) && (selectedHero.selectedAbility.isInfCharge)) {
                CheckForInfChargeActivation();
            }
            else if ((selectedHero.currentBattleState != Hero.BattleState.Charge) && (selectedHero.currentBattleState != Hero.BattleState.Barrage)) {
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

        if (hero.currentBattleState == Hero.BattleState.Wait) {
            return;
        }

        else if(hero.currentBattleState == Hero.BattleState.Target) {
            if (hero.currentAbility != null) {
                hero.currentAbility.AbilityMap();
            }
            if(Input.GetMouseButtonDown(0)) {
                targetingManager.CastSelecterRay(hero);
            }
        }

        else if(hero.currentBattleState == Hero.BattleState.TargetAll) {
            targetingManager.RefreshTargetAllEnemies(hero);
            hero.currentAbility.SetBattleState();
            hero.currentAbility.AbilityMap();
        }

        else if(hero.currentBattleState == Hero.BattleState.ReTarget) {
            targetingManager.ReTargetRandomEnemy(hero); 
        }

        else if(hero.currentBattleState == Hero.BattleState.Charge) {
            hero.currentAbility.CheckCharge();
        }

        else if(hero.currentBattleState == Hero.BattleState.Burst) {
            hero.currentAbility.AbilityMap();
        }

        else if(hero.currentBattleState == Hero.BattleState.Barrage) {
            hero.currentAbility.AbilityMap();
        }

        else if(hero.currentBattleState == Hero.BattleState.InfCharge) {
            return;
        }

        else if(hero.currentBattleState == Hero.BattleState.InfBarrage) {
            hero.currentAbility.AbilityMap();
        }

        else if(hero.currentBattleState == Hero.BattleState.Dead) {
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

        if((Input.GetKeyDown(KeyCode.Q)) && (selectedHero.abilityOne.cooldownEndTimer <= Time.time)) {
            selectedHero.selectedAbility = selectedHero.abilityOne;
            ExecuteAbilitySelection(selectedHero);
        }

        if((Input.GetKeyDown(KeyCode.W)) && (selectedHero.abilityTwo.cooldownEndTimer <= Time.time)) {
            selectedHero.selectedAbility = selectedHero.abilityTwo;
            ExecuteAbilitySelection(selectedHero);
        }

        if((Input.GetKeyDown(KeyCode.E)) && (selectedHero.abilityThree.cooldownEndTimer <= Time.time)) {
            selectedHero.selectedAbility = selectedHero.abilityThree;
            ExecuteAbilitySelection(selectedHero);
        }

    } //end CheckForHeroSelectionInput()

    void CheckForInfChargeActivation() {

        if((Input.GetKeyDown(KeyCode.Q)) && (selectedHero.selectedAbility == selectedHero.abilityOne)) {
            SelectedToTargeting(selectedHero);
            ExecuteInfChargeTargeting(selectedHero);
        }

        if((Input.GetKeyDown(KeyCode.W)) && (selectedHero.selectedAbility == selectedHero.abilityTwo)) {
            SelectedToTargeting(selectedHero);
            ExecuteInfChargeTargeting(selectedHero);
        }

        if((Input.GetKeyDown(KeyCode.E)) && (selectedHero.selectedAbility == selectedHero.abilityThree)) {
            SelectedToTargeting(selectedHero);
            ExecuteInfChargeTargeting(selectedHero);
        }

    } //end CheckForInfChargeActivation()


//Ability handling functions

    public void ExecuteAbilitySelection(Hero hero) {

        if(hero.selectedAbility.isInfCharge) {
            hero.selectedAbility.InitAbility();
            return;
        }
        else if(hero.selectedAbility.requiresTargeting) {
            SelectedToTargeting(hero);
            hero.currentBattleState = Hero.BattleState.Target;
        }
        else {
            SelectedToTargeting(hero);
            targetingManager.SortUntargetedType(hero);
        }

    } //end ExecuteAbilitySelection()

    public void ExecuteInfChargeTargeting(Hero hero) {
        if(hero.targetingAbility.requiresTargeting) {
            hero.currentBattleState = Hero.BattleState.Target;
        }
        else {
            targetingManager.SortUntargetedType(hero);
        }
    } //end ExecuteInfChargeTargeting()

    public void ExecuteAbility(Hero hero) {

        if (hero.currentAbility.isInfCharge) {
            hero.currentAbility.AbilityMap();
        }
        else if(hero.currentAbility.requiresCharge) {
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
            hero.selectedAbility = null;
            hero.currentAbility = null;
            hero.targetingAbility = null;
        }
    } //end ResetHeroes()

    public void ResetHeroHealths() {

        foreach(Hero hero in heroList) {
            hero.currentHealth = hero.maxHealth;
        } 
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


    
