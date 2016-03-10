﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using BattleObjects;
using Heroes;
using Enemies;
using Effects;


namespace Abilities {

    public abstract class Ability : MonoBehaviour {

        //Name and hero

        public string abilityName {
            get; set;
        }

        public Hero abilityOwner;
        public Effect effectApplied;
            

        //Ability type-defining variables

        public bool requiresCharge {
            get; set;
        }

        public bool requiresTargeting {
            get; set;
        }

        public bool isInfBarrage {
            get; set;
        }
        
        public bool isInfCharge {
            get; set;
        }

        public bool retainsInfCharge {
            get; set;
        }
        
        public bool hasCooldown {
            get; set;
        }
        
        public DamageType primaryDamageType {
            get; set;
        }

        public enum DamageType {
            Null,
            Physical,
            Magical,
            Healing
        }

        public TargetScope targetScope {
            get; set;
        }

        public enum TargetScope {
            Null,
            Untargeted,
            SingleHero,
            SingleEnemy,
            SingleHeroOrEnemy,
            AllHeroes,
            AllEnemies,
            AllHeroesOrAllEnemies,
            FreeTargetAOE
        }


        //Targeting

        public Enemy targetEnemy {
            get; set;
        }

        public Hero targetHero {
            get; set;
        }

        public List<BattleObject> targetBattleObjectList = new List<BattleObject>();

        //Timekeeping

        public float chargeDuration {
            get; set;
        }

        public float chargeEndTimer {
            get; set;
        }

        public float infChargeStartTimer {
            get; set;
        }

        public float abilityDuration {
            get; set;
        }

        public float abilityEndTimer {
            get; set;
        }

        public float cooldownDuration {
            get; set;
        }

        public float cooldownEndTimer {
            get; set;
        }



        //Proc handlers 
            //NOTE: useful for single-proc-type abilities, which will be MOST of them, 
            //but abilities with multiple procs will have to create their own variables 
            //and reflect this in the AbilityMap().

        public float nextProcTimer {
            get; set;
        }

        public int procCounter {
            get; set;
        }

        public int procLimit {
            get; set;
        }

        public float procSpacing {
            get; set;
        }

        public float procDamage {
            get; set;
        }

        public float procHeal {
            get; set;
        }

        public float infProcMultiplier {
            get; set;
        }


        //HitManager variables

        public float critChance {
            get; set;
        }

        public float critMultiplier {
            get; set;
        }

        public float physicalPenetration {
            get; set;
        }

        public float magicalPenetration {
            get; set;
        }

        public float physicalAccuracy {
            get; set;
        }

        public float magicalAccuracy {
            get; set;
        }

        public float physicalFinesse {
            get; set;
        }

        public float magicalFinesse {
            get; set;
        }

        
        //Other stuff, not used for now


        public string resource {
            get; set;
        }

        public int cost {
            get; set;
        }


        //Ability constructor

        public Ability() {

            abilityOwner = null;

            requiresCharge = true;
            requiresTargeting = true;
            isInfBarrage = false;
            isInfCharge = false;
            retainsInfCharge = false;
            hasCooldown = true;

            targetScope = TargetScope.Null;

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

            resource = "";
            cost = 0;
            primaryDamageType = DamageType.Null;

        } //end constructor



        //Virtual functions (to be overridden in each ability as needed)

        //Key functions (these will require instructions in every ability, or at least each ability class...once i make those...

        public virtual void AbilityMap() {
            if (targetEnemy == null) {
                abilityOwner.currentBattleState = Hero.BattleState.ReTarget;
            }
        }

        public virtual void SetBattleState() { }
        public virtual void ClearTargeting() { }

        public virtual void InitCharge() {
            abilityOwner.canTakeCommands = false;
            chargeEndTimer = Time.time + chargeDuration;
            abilityOwner.currentBattleState = Hero.BattleState.Charge;
        }


        public virtual void CheckCharge() {
            if (chargeEndTimer <= Time.time) {
                InitAbility();
            }
        } //end CheckCharge()


        public virtual void InitAbility() {
            SetBattleState();
            if((abilityOwner.currentBattleState == Hero.BattleState.InfBarrage) | (abilityOwner.currentBattleState == Hero.BattleState.InfCharge)) {
                abilityOwner.canTakeCommands = true;
            }
            if(isInfBarrage == false) {
                abilityEndTimer = Time.time + abilityDuration;
            }
            if(isInfCharge) {
                infChargeStartTimer = Time.time;
            }
            if(targetScope == Ability.TargetScope.AllEnemies) {
                abilityOwner.currentBattleState = Hero.BattleState.TargetAll;
            }

        } //end InitAbility()


        public virtual void ExitAbility() {
            //ClearTargeting();
            if(hasCooldown) {
                cooldownEndTimer = Time.time + cooldownDuration;
            }
            abilityOwner.currentAbility = null;
            abilityOwner.canTakeCommands = true;
            abilityOwner.currentBattleState = Hero.BattleState.Wait;
        } //end ExitAbility()



        //Proc functions

        public virtual void DamageProc (BattleObject defender, float damage) {
            int damageToApply = Mathf.RoundToInt(HitManager.ApplyResist(abilityOwner, defender, this, damage));
            defender.currentHealth -= damageToApply;
            defender.SpawnDamageText(damageToApply);
        }

        public virtual void CritDamageProc (BattleObject defender, float damage) {
            int damageToApply = Mathf.RoundToInt(HitManager.ApplyResist(abilityOwner, defender, this, (damage * critMultiplier)));
            defender.currentHealth -= damageToApply;
            defender.SpawnDamageText(damageToApply);
        }

        public virtual void BlockDamageProc (BattleObject defender, float damage) {

            float blockModifier = 0;

            if (primaryDamageType == DamageType.Physical) {
                blockModifier = defender.physicalBlockModifier;
            }
            else if (primaryDamageType == DamageType.Magical) {
                blockModifier = defender.magicalBlockModifier;
            }

            int damageToApply = Mathf.RoundToInt(HitManager.ApplyResist(abilityOwner, defender, this, (damage * (1 - (blockModifier / 100)))));
            defender.currentHealth -= damageToApply;
            defender.SpawnDamageText(damageToApply);

        } //end BlockDamageProc


        public virtual void DetermineHitOutcomeSingle(Hero attacker, BattleObject defender) {
            
            if (HitManager.DetermineEvasion(attacker, defender, this) == true) {
                defender.SpawnMissText();
                Debug.Log("Miss");
                return;
            }
            else if (HitManager.DetermineBlock(attacker, defender, this) == true) {
                BlockDamageProc(defender, procDamage);
                Debug.Log("Block");
                return;
            }
            else if (HitManager.DetermineCrit(attacker, defender, this) == true) {
                CritDamageProc(defender, procDamage);
                Debug.Log("Crit");
                return;
            }
            else {
                Debug.Log("Hit");
                DamageProc(defender, procDamage);
            }

        } //end DamageProcSingle(2)
        

        public virtual void DetermineHitOutcomeMultiple(Hero attacker) {

            foreach (BattleObject defender in targetBattleObjectList) {
                DetermineHitOutcomeSingle(attacker, defender);
            } //end foreach

        } //end DamageProcMultiple()


        public virtual void InfDamageProc(Hero attacker, Enemy defender, float multiplier) {
            int damage = Mathf.RoundToInt(multiplier * (Time.time - infChargeStartTimer));
            defender.currentHealth -= damage;
            defender.SpawnDamageText(damage);
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
            foreach (BattleObject healee in targetBattleObjectList)
            {
                if ((healee.currentHealth + healer.currentAbility.procHeal) <= healee.maxHealth)
                {
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
            if(healee.currentHealth + (multiplier * (Time.time - infChargeStartTimer)) <= healee.maxHealth) {
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
        }

        public virtual void ApplyEffectMultiple(Effect effect) {
            effect.CreateEffectMultiple(targetBattleObjectList);
        }
        
    } //end Ability class

} //end Ability namespace