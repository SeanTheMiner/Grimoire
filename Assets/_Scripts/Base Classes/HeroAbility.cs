using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Abilities;
using BattleObjects;
using Heroes;
using Enemies;
using Effects;
using Procs;


public class HeroAbility : Ability {

    public int manaCost {
        get; set;
    }

    public Hero abilityOwner;
    public Effect coreEffectApplied;

    public GameObject associatedTargeter;

    public AbilityButtonManager abilityButtonManager;
    public BattleManager battleManager;
    public EffectManager effectManager;

    //ability-defining vools


    public bool requiresCharge {
        get; set;
    }

    public bool requiresTargeting {
        get; set;
    }

    public bool costsMana {
        get; set;
    }

    public bool isInfBarrage {
        get; set;
    }

    public bool isInfCharging {
        get; set;
    }

    public bool retainsInfCharge {
        get; set;
    }

    public bool hasCooldown {
        get; set;
    }

    public bool canBeDefault {
        get; set;
    }
    
    public bool appliesCoreEffect {
        get; set;
    }

    public bool isRevive {
        get; set;
    }

    public bool canBreakStun {
        get; set;
    }


    public AbilityType abilityType {
        get; set;
    }

    public enum AbilityType {
        Burst,
        Barrage,
        InfCharge,
        InfBarrage,
        Toggle
    }
    
    public TargetScope targetScope;

    public enum TargetScope {
        Untargeted,
        Self,
        SingleHero,
        SingleEnemy,
        SingleHeroOrEnemy,
        AllHeroes,
        AllEnemies,
        AllHeroesOrAllEnemies,
        FreeTargetAOE,
        CenteredAOE
    }


    public float radiusOfAOE {
        get; set;
    }


    //Additional timekeeping

    public float infChargeStartTimer {
        get; set;
    }

    public float infProcMultiplier {
        get; set;
    }


    //Constructor

    public HeroAbility() {

        abilityOwner = null;
        manaCost = 0;

        requiresCharge = true;
        requiresTargeting = true;
        costsMana = true;
        retainsInfCharge = false;
        hasCooldown = true;
        isInfCharging = false;
        canBeDefault = true;
        appliesCoreEffect = false;
        isRevive = false;
        canBreakStun = false;

        chargeDuration = 0.0f;
        chargeEndTimer = 0.0f;

        abilityDuration = 0.0f;
        abilityEndTimer = 0.0f;

        cooldownDuration = 0.0f;
        cooldownEndTimer = 0.0f;

        nextProcTimer = 0.0f;
        procCounter = 0;
        procLimit = 0;
        procSpacing = 0.0f;
        procDamage = 0.0f;
        procHeal = 0.0f;

    } //end constructor


    //Ability navigation functions


    public virtual void SetCoreEffectApplied() {}
    

    public virtual void InitAbility() {

        SetCoreEffectApplied();
        abilityButtonManager = GameObject.Find("AbilityButtonManager").GetComponent<AbilityButtonManager>();
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();

        if (abilityType == AbilityType.InfCharge) {
            infChargeStartTimer = Time.time;
            abilityOwner.currentBattleState = Hero.BattleState.InfCharge;
            isInfCharging = true;
            return;
        }

        else if (requiresTargeting) {

            if (targetScope == TargetScope.FreeTargetAOE) {
                ActivateAOETargeter();
            }
            abilityOwner.currentBattleState = Hero.BattleState.Target;
        }

        else if (requiresCharge) {
            InitCharge();
        }

        else {
            if (costsMana) {
                ApplyManaCost();
            }
            AbilityMap();
        }

    } //end InitAbility()


    public void InitDefaultAbility() {

        SetCoreEffectApplied();
        abilityButtonManager = GameObject.Find("AbilityButtonManager").GetComponent<AbilityButtonManager>();
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();

        if (abilityType == AbilityType.InfCharge) {
            infChargeStartTimer = Time.time;
            abilityOwner.currentBattleState = Hero.BattleState.InfCharge;
            isInfCharging = true;
            return;
        }

        if (requiresTargeting) {
            if (targetScope == TargetScope.SingleEnemy) {
                targetEnemy = (Enemy)targetingManager.TargetRandomEnemy();
            }
            else if (targetScope == TargetScope.SingleHero) {
                targetHero = (Hero)targetingManager.TargetRandomHero();
            }
        }

        if (requiresCharge) {
            InitCharge();
        }

        else {
            if (costsMana) {
                ApplyManaCost();
            }
            AbilityMap();
        }
        
    } //end InitDefaultAbility()



    public virtual void TargetInfCharge() {
        targetingManager.SortTargetingType(this);
    }


    public virtual void CastRay() {
        targetingManager.CastSelecterRay(this);
    }


    public virtual void ActivateAOETargeter() {
        associatedTargeter = (GameObject)Object.Instantiate(Resources.Load("AOEFreetargeterPrefab"), Input.mousePosition, Quaternion.identity);
        associatedTargeter.transform.localScale = new Vector3(radiusOfAOE, 0.05f, radiusOfAOE);
    }


    public virtual void PlaceAOETargeter() {
        associatedTargeter.GetComponent<AOETargeterController>().PlaceTargeter();
        if (requiresCharge) {
            InitCharge();
        }
        else {
            AbilityMap();
        }
    }


    public List<BattleObject> CheckAOETargets() {
        return associatedTargeter.GetComponent<AOETargeterController>().battleObjectList;
    }


    public List<BattleObject> CheckAOETargetsOnCenter(BattleObject centerTarget) {
        
        List<BattleObject> listToReturn = new List<BattleObject>();
        Collider[] gameObjectsFound = Physics.OverlapSphere(centerTarget.transform.position, (radiusOfAOE/2));

        foreach (Collider collider in gameObjectsFound) {
            if (collider.gameObject.tag == "Enemy") {
                listToReturn.Add(collider.gameObject.GetComponent<Enemy>());
            }
        }
     
        return listToReturn;

    } //end CheckAOETargetsOnCenter(1)


    public virtual void InitCharge() {
        abilityOwner.canTakeCommands = false;
        chargeEndTimer = Time.time + chargeDuration;
        abilityOwner.currentBattleState = Hero.BattleState.Charge;
    }


    public virtual void CheckCharge() {
        if (chargeEndTimer <= Time.time) {
            if (abilityType == AbilityType.Barrage) {
                abilityEndTimer = Time.time + abilityDuration;
            }
            if (abilityType != AbilityType.InfBarrage) {
                abilityOwner.currentBattleState = Hero.BattleState.Ability;
            }
            else {
                abilityOwner.currentBattleState = Hero.BattleState.InfBarrage;
                abilityOwner.canTakeCommands = true;
            }

            if (costsMana) {
                ApplyManaCost();
            }

            AbilityMap();
        } //end if chargeEndTimer <= Time.time
    } //end CheckCharge()


    public virtual void ApplyManaCost() {
        abilityOwner.currentMana -= manaCost;
    } //end ApplyManaCost()


    public void UpdateInfBarrageMask(float nextProcTimer, float spacing) {

        if (abilityOwner != battleManager.selectedHero) {
            return;
        }
        DetermineInfBarrageMask().fillAmount = (1 - ((nextProcTimer - Time.time)/ spacing));
    } 


    public Image DetermineInfBarrageMask() {

        Image maskToUpdate;

        if (abilityOwner.abilityOne == this) {
            maskToUpdate = abilityButtonManager.infBarrageMaskOne;
        }
        else if (abilityOwner.abilityTwo == this) {
            maskToUpdate = abilityButtonManager.infBarrageMaskTwo;
        }
        else if (abilityOwner.abilityThree == this) {
            maskToUpdate = abilityButtonManager.infBarrageMaskThree;
        }
        else if (abilityOwner.abilityFour == this) {
            maskToUpdate = abilityButtonManager.infBarrageMaskFour;
        }
        else if (abilityOwner.abilityFive == this) {
            maskToUpdate = abilityButtonManager.infBarrageMaskFive;
        }
        else {
            maskToUpdate = abilityButtonManager.infBarrageMaskSix;
        }

        return maskToUpdate;

    } //end UpdateInfBarrageMask()


    public virtual void AbilityMap() {

        // This is where everything defining the ability goes. 

    }


    public virtual void ExitAbility() {

        ClearTargeting();

        if (hasCooldown) {
            cooldownEndTimer = Time.time + cooldownDuration;
        }

        abilityOwner.currentAbility = null;
        abilityOwner.canTakeCommands = true;
        abilityOwner.currentBattleState = Hero.BattleState.Wait;

        if (associatedTargeter != null) {
            MonoBehaviour.Destroy(associatedTargeter);
        }

    } //end ExitAbility()


    public virtual void ClearTargeting() {
        targetEnemy = null;
        targetHero = null;
        targetBattleObjectList.Clear();
    }


    public virtual void CheckTarget() {

        if ((targetScope == TargetScope.SingleEnemy) && (targetEnemy == null)) {
            targetEnemy = (Enemy)targetingManager.TargetRandomEnemy();
        }
        else if ((targetScope == TargetScope.SingleHero) && (targetHero == null)) {
            targetHero = (Hero)targetingManager.TargetRandomHero();
        }
        else if (targetScope == TargetScope.FreeTargetAOE) {
            CheckAOETargets();
        }


    } //end CheckTarget()




    //Hit functions


    public virtual void InfDetermineHitOutcomeSingle(BattleObject attacker, BattleObject defender, HeroAbility ability, DamageProc damageProc) {

        HitManager.HitOutcome hitOutcome = HitManager.DetermineEvasionAndBlock(attacker, defender, this, damageProc);

        if (hitOutcome == HitManager.HitOutcome.Evade) {
            defender.SpawnMissText(damageProc.damageType);
            return;
        }
        if (hitOutcome == HitManager.HitOutcome.Block) {
            damageProc.ApplyBlockDamageProc(attacker, defender);
            return;
        }

        hitOutcome = HitManager.DetermineCrit(attacker, defender, damageProc);

        if (hitOutcome == HitManager.HitOutcome.Crit) {
            damageProc.ApplyInfDamageProc(attacker, defender, ability);
            //This eventually needs to be set up to crit, actually
            return;
        }
        else {
            damageProc.ApplyInfDamageProc(attacker, defender, ability);
            return;
        }

    } //End DetermineHitOutComeSingle (3)


    public virtual void InfDetermineHitOutcomeMultiple(BattleObject attacker, DamageProc damageProc, HeroAbility ability) {

        foreach (BattleObject defender in targetBattleObjectList) {
            InfDetermineHitOutcomeSingle(attacker, defender, ability, damageProc);
        } //end foreach

    } //end DamageProcMultiple()
    

    public float ApplySpacing(float spacingToApply) {

        float timerToReturn;

        if (abilityDamageType == AbilityDamageType.Physical) {
            timerToReturn = Time.time + (spacingToApply / abilityOwner.physicalAttackSpeedMultMod);
        }
        else if (abilityDamageType == AbilityDamageType.Magical) {
            timerToReturn = Time.time + (spacingToApply / abilityOwner.magicalAttackSpeedMultMod);
        }
        else if (abilityDamageType == AbilityDamageType.Healing) {
            timerToReturn = Time.time + (spacingToApply / abilityOwner.healSpeedMultMod);
        }
        else {
            timerToReturn = Time.time + spacingToApply;
        }

        return timerToReturn;

    } //end ApplySpacing(2)
    

    public EffectController FindEffectController (Effect effectToCheckFor) {

        effectManager = GameObject.Find("EffectManager").GetComponent<EffectManager>();

        foreach (EffectController effectController in effectManager.activeEffectControllerList) {
            if (effectController.effectApplied == effectToCheckFor) {
                return effectController;
            }
        }
        return null;

    } //end FindEffectController(1)

    
} //end HeroAbility class
