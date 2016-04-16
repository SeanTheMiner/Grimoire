using UnityEngine;
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


    public virtual void InitAbility() {

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

        if (abilityType == AbilityType.InfCharge) {
            infChargeStartTimer = Time.time;
            abilityOwner.currentBattleState = Hero.BattleState.InfCharge;
            isInfCharging = true;
            return;
        }

        if (requiresTargeting) {
            if (targetScope == TargetScope.SingleEnemy) {
                targetingManager.TargetRandomEnemy(this);
            }
            else if (targetScope == TargetScope.SingleHero) {
                targetingManager.TargetRandomHero(this);
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
        associatedTargeter = (GameObject)MonoBehaviour.Instantiate(Resources.Load("AOEFreetargeterPrefab"), Input.mousePosition, Quaternion.identity);
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


    public virtual void CheckAOETargets() {
        foreach (BattleObject enemy in associatedTargeter.GetComponent<AOETargeterController>().battleObjectList) {
            targetBattleObjectList.Add(enemy);
        }
    }

    

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
            targetingManager.TargetRandomEnemy(this);
        }
        else if ((targetScope == TargetScope.SingleHero) && (targetHero == null)) {
            targetingManager.TargetRandomHero(this);
        }
        else if (targetScope == TargetScope.FreeTargetAOE) {
            CheckAOETargets();
        }


    } //end CheckTarget()




    //Hit functions


    public virtual void InfDetermineHitOutcomeSingle(BattleObject attacker, BattleObject defender, DamageProc damageProc, HeroAbility ability) {

        HitManager.HitOutcome hitOutcome = HitManager.DetermineEvasionAndBlock(attacker, defender, this);

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
            InfDetermineHitOutcomeSingle(attacker, defender, damageProc, ability);
        } //end foreach

    } //end DamageProcMultiple()





    //recently ported to Ability

    /*

public virtual void DetermineHitOutcomeSingle(BattleObject attacker, BattleObject defender, DamageProc damageProc) {

    HitManager.HitOutcome hitOutcome = HitManager.DetermineEvasionAndBlock(attacker, defender, this);

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
        damageProc.ApplyCritDamageProc(attacker, defender);
        return;
    }
    else {
        damageProc.ApplyDamageProc(attacker, defender);
        return;
    }

} //End DetermineHitOutComeSingle (3)


public virtual void DetermineHitOutcomeMultiple(BattleObject attacker, DamageProc damageProc) {

    foreach (BattleObject defender in targetBattleObjectList) {
        DetermineHitOutcomeSingle(attacker, defender, damageProc);
    } //end foreach

} //end DamageProcMultiple()


*/
    //End recently ported to Ability



    /*
    public virtual void DetermineHitOutcomeSingleOld(Hero attacker, BattleObject defender) {

        HitManager.HitOutcome hitOutcome = HitManager.DetermineEvasionAndBlock(attacker, defender, this);

        if (hitOutcome == HitManager.HitOutcome.Evade) {
            defender.SpawnMissText(primaryDamageType);
            return;
        }
        if (hitOutcome == HitManager.HitOutcome.Block) {
            ApplyBlockDamageProc(defender, procDamage);
            return;
        }

        hitOutcome = HitManager.DetermineCrit(attacker, defender, this);

        if (hitOutcome == HitManager.HitOutcome.Crit) {
            ApplyCritDamageProc(defender, procDamage);
            return;
        }
        else {
            ApplyDamageProc(defender, procDamage);
            return;
        }


    } //end DamageProcSingle(2)
    */




    /*

    public virtual void ApplyDamageProc(BattleObject defender, float damage) {

        int damageToApply = Mathf.RoundToInt(HitManager.ApplyResist(abilityOwner, defender, this, damage));
        defender.currentHealth -= damageToApply;
        defender.SpawnDamageText(damageToApply, primaryDamageType);

    }


    public virtual void ApplyCritDamageProc(BattleObject defender, float damage) {

        int damageToApply = Mathf.RoundToInt(HitManager.ApplyResist(abilityOwner, defender, this, (damage * critMultiplier)));
        defender.currentHealth -= damageToApply;
        defender.SpawnDamageText(damageToApply, primaryDamageType);
        //could call SpawnCritText, or not
    }


    public virtual void ApplyBlockDamageProc(BattleObject defender, float damage) {

        float blockModifier = 0;

        if (primaryDamageType == DamageType.Physical) {
            blockModifier = defender.physicalBlockModifier;
        }
        else if (primaryDamageType == DamageType.Magical) {
            blockModifier = defender.magicalBlockModifier;
        }

        int damageToApply = Mathf.RoundToInt(HitManager.ApplyResist(abilityOwner, defender, this, (damage * (1 - (blockModifier / 100)))));
        defender.currentHealth -= damageToApply;
        defender.SpawnBlockText(damageToApply, primaryDamageType);

    } //end BlockDamageProc


    public virtual void InfDamageProc(Hero attacker, Enemy defender, float multiplier) {
        isInfCharging = false;
        int damage = Mathf.RoundToInt(multiplier * (Time.time - infChargeStartTimer));
        defender.currentHealth -= damage;
        defender.SpawnDamageText(damage, primaryDamageType);
    } //end InfDamageProc()


    public virtual void HealProcSingle(Hero healer, Hero healee) {
        int heal;
        if ((healee.currentHealth + healer.currentAbility.procHeal) <= healee.maxHealth) {
            heal = Mathf.RoundToInt(healer.currentAbility.procHeal);
        }
        else {
            heal = Mathf.RoundToInt(healee.maxHealth - healee.currentHealth);
        }
        healee.currentHealth += heal;
        healee.SpawnHealText(heal);
    } //end HealProcSingle


    public virtual void HealProcMultiple(Hero healer) {
        int heal;
        foreach (BattleObject healee in targetBattleObjectList) {
            if ((healee.currentHealth + healer.currentAbility.procHeal) <= healee.maxHealth) {
                heal = Mathf.RoundToInt(healer.currentAbility.procHeal);
            }
            else {
                heal = Mathf.RoundToInt(healee.maxHealth - healee.currentHealth);
            }
            healee.currentHealth += heal;
            healee.SpawnHealText(heal);
        }
    } //end HealProcMultiple


    public virtual void InfHealProc(Hero healer, Hero healee, float multiplier) {

        int heal;
        if (healee.currentHealth + (multiplier * (Time.time - infChargeStartTimer)) <= healee.maxHealth) {
            heal = Mathf.RoundToInt(multiplier * (Time.time - infChargeStartTimer));
        }
        else {
            heal = Mathf.RoundToInt(healee.maxHealth - healee.currentHealth);
        }
        healee.currentHealth += heal;
        healee.SpawnHealText(heal);
    }


    //Effect functions

    public virtual void ApplyEffectSingle(Effect effect, BattleObject target) {
        effect.CreateEffectSingle(target);
        if (effect.effectType == Effect.EffectType.Stacking) {
            effect.InitStacks(effectStacksApplied);
        }
    } //end ApplyEffectSingle(2)


    public virtual void ApplyEffectMultiple(Effect effect) {
        effect.CreateEffectMultiple(targetBattleObjectList);
    }

    */

} //end HeroAbility class
