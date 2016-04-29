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
    public AbilityButtonManager abilityButtonManager;
    
    public float battleTimer = 0.0f;
    public static System.Random randomer;

    public GameObject AOETargeter;

    //Hero variables

    public Hero heroObjectOne, heroObjectTwo, heroObjectThree, heroObjectFour,
        selectedHero, queuedHero
        ;

    public Champion champion;

    public Ability targetingAbility;
    
    public Enemy enemyObjectOne;
    public Enemy enemyObjectTwo;
    public Enemy enemyObjectThree;

    public List<Hero> heroList = new List<Hero>();
    public List<Enemy> enemyList = new List<Enemy>();
    public List<Enemy> enemyToRemoveList = new List<Enemy>();
   
    public GameObject[] allHeroes;
    public GameObject[] allEnemies;
    public GameObject[] allHeroesAndEnemies;

    //public GameObject AOETargeter;

    //LET'S GET IT STARTED IN HERE
    void Start() {

        PopulateHeroList();
        PopulateEnemyList();

        foreach (Hero hero in heroList) {
            hero.BattleStart();
        }

        ResetHeroes();
        ResetEnemyHealths();

        InvokeRepeating("ApplyHealthAndManaRegen", 0, 0.2f);
        
        battleDisplayManager.UpdateHealthText();
        battleDisplayManager.NoHeroSelected();
        battleDisplayManager.InitNameText();
        
        debugDisplayManager.InitDebugText();
        debugDisplayManager.UpdateDebugText();

        //AOETargeter.SetActive(false);

    } //end Start()


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

            abilityButtonManager.UpdateSelectedHeroButtons(selectedHero);

            if (selectedHero.canTakeCommands) {
                CheckForAbilitySelectionInput();
                CheckForRevivalInput();
            }
            if (Input.GetKey(KeyCode.LeftShift)) {
                CheckForDefaultAbilitySelectionInput();
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
        
        foreach (Enemy enemy in enemyToRemoveList) {
            enemyList.Remove(enemy);
        }
        
        enemyToRemoveList.Clear();
        
        if (enemyList.Count <= 0) {
            BattleWon();
        }

     
        //DEBUG FUNCTIONS

        if(Input.GetKeyDown(KeyCode.Z)) {
            DebugAllHeroDamage();
        }

        if(Input.GetKeyDown(KeyCode.X)) {
            DebugSelectedHeroKill();
        }

        if(Input.GetKeyDown(KeyCode.C)) {
            if (selectedHero != null) {
                DebugResetSelectedHeroCooldowns(selectedHero);
            }
        }
        
    } //end Update

    

    //BattleState switch. Not actually a switch, but this  decides what a hero does in every frame based on their battlestate.
        //Some notes - Target only applies if a target is being chosen. 
        //Abilities that don't need player input for a target skip the targeting BattleState, and call the appropriate function on TargetManager by themselves.
        //At some point, BattleState.Burst might work this way too. For now, it's fine.

    void CheckBattleState(Hero hero) {

        if ((hero.currentBattleState == Hero.BattleState.InfCharge) 
            | (hero.currentBattleState == Hero.BattleState.Dead)) {
            return;
        }

        else if (hero.currentBattleState == Hero.BattleState.Wait) {
            if ((hero.defaultAbility != null) && (hero.defaultAbility.cooldownEndTimer < Time.time) && (hero.currentMana >= hero.defaultAbility.manaCost)) {
                hero.currentAbility = hero.defaultAbility;
                hero.currentAbility.InitDefaultAbility();
            }
        }
        
        else if (hero.currentBattleState == Hero.BattleState.Target) {

            if (hero.currentAbility.targetScope == HeroAbility.TargetScope.FreeTargetAOE) {
                if (Input.GetMouseButtonDown(0)) {
                    selectedHero.currentAbility.PlaceAOETargeter();
                }
            }
            else {
                if (Input.GetMouseButtonDown(0)) {
                    selectedHero.currentAbility.CastRay();
                }
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

        else if (hero.currentBattleState == Hero.BattleState.RevTarget) {
            if (Input.GetMouseButtonDown(0)) {
                selectedHero.targetingManager.CastRevivalSelecterRay(selectedHero);
            }
        }

        else if (hero.currentBattleState == Hero.BattleState.Reviving) {
            hero.CheckRevivalCharge();
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

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            selectedHero = heroObjectThree;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            selectedHero = heroObjectFour;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (!Input.GetKey(KeyCode.LeftShift))) {
            CancelAbility(selectedHero);
        }
        
    } //end CheckForHeroSelectionInput()


    void CheckForAbilitySelectionInput() {

        HeroAbility abilityToApply = null;

        if ((Input.GetKeyDown(KeyCode.Q)) && (selectedHero.abilityOne.cooldownEndTimer <= Time.time) && (selectedHero.currentMana >= selectedHero.abilityOne.manaCost)) {
            abilityToApply = selectedHero.abilityOne;
        }
        else if ((Input.GetKeyDown(KeyCode.W)) && (selectedHero.abilityTwo.cooldownEndTimer <= Time.time) && (selectedHero.currentMana >= selectedHero.abilityTwo.manaCost)) {
            abilityToApply = selectedHero.abilityTwo;
        }
        else if ((Input.GetKeyDown(KeyCode.E)) && (selectedHero.abilityThree.cooldownEndTimer <= Time.time) && (selectedHero.currentMana >= selectedHero.abilityThree.manaCost)) {
            abilityToApply = selectedHero.abilityThree;
        }
        else if ((Input.GetKeyDown(KeyCode.A)) && (selectedHero.abilityFour.cooldownEndTimer <= Time.time) && (selectedHero.currentMana >= selectedHero.abilityFour.manaCost)) {
            abilityToApply = selectedHero.abilityFour;
        }
        else if ((Input.GetKeyDown(KeyCode.S)) && (selectedHero.abilityFive.cooldownEndTimer <= Time.time) && (selectedHero.currentMana >= selectedHero.abilityFive.manaCost)) {
            abilityToApply = selectedHero.abilityFive;
        }
        else if ((Input.GetKeyDown(KeyCode.D)) && (selectedHero.abilitySix.cooldownEndTimer <= Time.time) && (selectedHero.currentMana >= selectedHero.abilitySix.manaCost)) {
            abilityToApply = selectedHero.abilitySix;
        }
        

        if (abilityToApply != null) {
            if (selectedHero.currentBattleState != Hero.BattleState.InfCharge) {
                
                if ((selectedHero.currentAbility != null) && (selectedHero.currentAbility.associatedTargeter != null)) {
                    Destroy(selectedHero.currentAbility.associatedTargeter);
                }

                selectedHero.currentAbility = abilityToApply;
                abilityToApply.InitAbility();
            }
            else if (abilityToApply == selectedHero.currentAbility) {
                selectedHero.currentAbility.targetingManager.SortTargetingType(selectedHero.currentAbility);
            }
        }
 
    } //end CheckForAbilitySelectionInput()
    

    void CheckForDefaultAbilitySelectionInput() {

        HeroAbility defaultAbilityToApply = null;
        
        if (Input.GetKeyDown(KeyCode.Q)) {
            defaultAbilityToApply = selectedHero.abilityOne;
        }
        else if (Input.GetKeyDown(KeyCode.W)) {
            defaultAbilityToApply = selectedHero.abilityTwo;
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            defaultAbilityToApply = selectedHero.abilityThree;
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            defaultAbilityToApply = selectedHero.abilityFour;
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            defaultAbilityToApply = selectedHero.abilityFive;
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            defaultAbilityToApply = selectedHero.abilitySix;
        }
        
        if ((defaultAbilityToApply != null) && (defaultAbilityToApply.canBeDefault)) {
            selectedHero.defaultAbility = defaultAbilityToApply;
        }
        else if (Input.GetKeyDown(KeyCode.Tab)) {
            selectedHero.defaultAbility = null;
        }

    } //end CheckForDefaultAbilitySelectionInput()


    bool CheckForDeadHeroes () {

        GameObject[] allHeroes = GameObject.FindGameObjectsWithTag("DeadHero");
        if (allHeroes.Length > 0) {
            return true;
        }
        else {
            return false;
        }

    } //end CheckForDeadHeroes()


    void CheckForRevivalInput () {
        
        if ((CheckForDeadHeroes()) && (selectedHero.tag != "DeadHero")) {
            if (Input.GetKeyDown(KeyCode.R)) {
                selectedHero.currentBattleState = Hero.BattleState.RevTarget;
            }
        }

    } //end CheckForRevivalInput()

    
//Ability handling functions

        
        
    public void CancelAbility(Hero hero) {

        hero.selectedAbility = null;
        hero.targetingAbility = null;
        hero.revivalTarget = null;

        if (hero.currentAbility != null) {

            if (hero.currentAbility.associatedTargeter != null) {
                Destroy(hero.currentAbility.associatedTargeter);
            }

            if (hero.currentBattleState == Hero.BattleState.Charge) {
                hero.currentAbility.chargeEndTimer = 0;
            }
            else if (hero.currentBattleState == Hero.BattleState.InfCharge) {
                hero.currentAbility.isInfCharging = false;
            }
            else if (hero.currentBattleState != Hero.BattleState.Target) {
                hero.currentAbility.cooldownEndTimer = Time.time + hero.currentAbility.cooldownDuration;
            }
        }
        
        hero.currentAbility = null;
        hero.canTakeCommands = true;
        hero.currentBattleState = Hero.BattleState.Wait;

    } //end CancelAbility(Hero)


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
            if (!hero.isDead) {
                if (hero.currentHealth < hero.maxHealth) {
                    hero.currentHealth += (hero.healthRegen / 5);
                }
                if (hero.currentMana < hero.maxMana) {
                    hero.currentMana += (hero.manaRegen / 5);
                }
            }
        } //end foreach hero

    } //end ApplyHealthRegen()


    void CheckIfEnemyIsDead(Enemy enemy) {

        if(enemy.currentHealth <= 0) {
            enemyToRemoveList.Add(enemy);
        }
    }


    void RemoveEnemy(Enemy enemy) {
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



//Debug functions

    void DebugAllHeroDamage() {

        foreach(Hero hero in heroList) {
            if (!hero.isDead) {
                hero.currentHealth -= 120;
                hero.SpawnDamageText(120, Procs.DamageProc.DamageType.Physical);
            }
        }
    }


    void DebugSelectedHeroKill () {

        if(selectedHero != null) {
            if (!selectedHero.isDead) {
                selectedHero.SetHeroToDead();
            }
            else {
                selectedHero.ReviveHeroPercentage(50);
            }
        }
    }


    private void DebugResetSelectedHeroCooldowns(Hero hero) {

        hero.abilityOne.cooldownEndTimer = 0;
        hero.abilityTwo.cooldownEndTimer = 0;
        hero.abilityThree.cooldownEndTimer = 0;
        hero.abilityFour.cooldownEndTimer = 0;
        hero.abilityFive.cooldownEndTimer = 0;
        hero.abilitySix.cooldownEndTimer = 0;

    } //end DebugResetSelectedHeroCooldowns(1)


} //end BattleManager



