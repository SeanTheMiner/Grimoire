using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BattleObjects;
using Heroes;
using Enemies;
using Effects;


namespace EnemyAbilities {


    public class EnemyAbility : MonoBehaviour {

        public TargetingManager targetingManager = new TargetingManager();


        public string abilityName {
            get; set;
        }

        public int enemyAbilityWeight {
            get; set;
        }
        
        public int enemyAbilityWeightMark {
            get; set;
        }


        public Enemy abilityOwner;
        public Effect effectApplied;

        public List<BattleObject> targetBattleObjectList = new List<BattleObject>();


        public bool requiresCharge {
            get; set;
        }

        public bool hasCooldown {
            get; set;
        }

        public DamageType primaryDamageType {
            get; set;
        }

        public enum DamageType {
            Physical,
            Magical,
            Healing,
            None
        }

        public TargetScope targetScope {
            get; set;
        }

        public enum TargetScope {
            Self,
            SingleHero,
            RandomHeroes,
            SingleEnemy,
            RandomEnemies,
            AllHeroes,
            AllEnemies,
        }

        public EnemyAbilityType enemyAbilityType {
            get; set;
        }

        public enum EnemyAbilityType {
            Burst,
            Barrage
        }


        public Enemy targetEnemy {
            get; set;
        }

        public Hero targetHero {
            get; set;
        }


        //Timekeeping

        public float chargeDuration {
            get; set;
        }

        public float chargeEndTimer {
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

        //Enemy Ability constructor

        public EnemyAbility() {

            abilityOwner = null;

            requiresCharge = true;
            hasCooldown = false;

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




        //ABILITY NAVIGATION FUNCTIONS


        public virtual void InitEnemyAbility() {
            
            if (requiresCharge) {
                chargeEndTimer = Time.time + chargeDuration;
                abilityOwner.currentEnemyState = Enemy.EnemyState.Charge;
            }
            else {
                TargetEnemyAbility();
                abilityEndTimer = Time.time + abilityDuration;
                EnemyAbilityMap();
            }
        } //end InitEnemyAbility

        
        public virtual void CheckEnemyCharge() {

            if (chargeEndTimer <= Time.time) {
                TargetEnemyAbility();
                if (enemyAbilityType == EnemyAbilityType.Barrage) {
                    abilityOwner.currentEnemyState = Enemy.EnemyState.Barrage;
                }
                abilityEndTimer = Time.time + abilityDuration;
                EnemyAbilityMap();
            } //end if charged

        } //end CheckEnemyCharge()


        public virtual void TargetEnemyAbility() {
            if ((targetScope != TargetScope.RandomHeroes) | (targetScope != TargetScope.RandomEnemies)) {
                targetingManager.EnemySortTargetingType(this);
            } 
        } //end TargetEnemyAbility()

       
        public virtual void EnemyAbilityMap() {
            
            //This defines everything the ability actually does.

        } //end EnemyAbilityMap()


        public virtual void ExitEnemyAbility() {

            ClearTargeting();

            if (hasCooldown) {
                cooldownEndTimer = Time.time + cooldownDuration;
            }

            abilityOwner.currentEnemyState = Enemy.EnemyState.Inactive;

        } //end ExitEnemyAbility()


        public virtual void ClearTargeting() {
            targetEnemy = null;
            targetHero = null;
            targetBattleObjectList.Clear();
        }

        public virtual void CheckTarget() {

            if ((targetScope == TargetScope.SingleEnemy) && (targetEnemy == null)) {
                targetingManager.EnemyTargetRandomEnemy(this);
            }
            else if ((targetScope == TargetScope.SingleHero) && (targetHero == null)) {
                targetingManager.EnemyTargetRandomHero(this);
            }

        } //end CheckTarget()


        //PROC FUNCTIONS


        public virtual void DamageProc(BattleObject defender, float damage) {

            int damageToApply = Mathf.RoundToInt(HitManager.EnemyApplyResist(abilityOwner, defender, this, damage));
            defender.currentHealth -= damageToApply;
            defender.SpawnDamageText(damageToApply);
        }

        public virtual void CritDamageProc(BattleObject defender, float damage) {

            int damageToApply = Mathf.RoundToInt(HitManager.EnemyApplyResist(abilityOwner, defender, this, (damage * critMultiplier)));
            defender.currentHealth -= damageToApply;
            defender.SpawnDamageText(damageToApply);
        }

        public virtual void BlockDamageProc(BattleObject defender, float damage) {

            float blockModifier = 0;

            if (primaryDamageType == DamageType.Physical) {
                blockModifier = defender.physicalBlockModifier;
            }
            else if (primaryDamageType == DamageType.Magical) {
                blockModifier = defender.magicalBlockModifier;
            }

            int damageToApply = Mathf.RoundToInt(HitManager.EnemyApplyResist(abilityOwner, defender, this, (damage * (1 - (blockModifier / 100)))));
            defender.currentHealth -= damageToApply;
            defender.SpawnDamageText(damageToApply);

        } //end BlockDamageProc


        public virtual void DetermineHitOutcomeSingle(Enemy attacker, BattleObject defender) {

            if (HitManager.EnemyDetermineEvasion(attacker, defender, this) == true) {
                defender.SpawnMissText();
                Debug.Log("Miss");
                return;
            }
            else if (HitManager.EnemyDetermineBlock(attacker, defender, this) == true) {
                BlockDamageProc(defender, procDamage);
                Debug.Log("Block");
                return;
            }
            else if (HitManager.EnemyDetermineCrit(attacker, defender, this) == true) {
                CritDamageProc(defender, procDamage);
                Debug.Log("Crit");
                return;
            }
            else {
                Debug.Log("Hit");
                DamageProc(defender, procDamage);
            }

        } //end DamageProcSingle(2)


        public virtual void DetermineHitOutcomeMultiple(Enemy attacker) {

            foreach (BattleObject defender in targetBattleObjectList) {
                DetermineHitOutcomeSingle(attacker, defender);
            } //end foreach

        } //end DamageProcMultiple()


        public virtual void HealProcSingle(Enemy healer, Enemy healee) {
            int heal;
            if ((healee.currentHealth + healer.currentEnemyAbility.procHeal) <= healee.maxHealth) {
                heal = Mathf.RoundToInt(healer.currentEnemyAbility.procHeal);
            }
            else {
                heal = Mathf.RoundToInt(healee.maxHealth - healee.currentHealth);
            }
            healee.currentHealth += heal;
            healee.SpawnHealText(heal);
        } //end HealProcSingle


        public virtual void HealProcMultiple(Enemy healer) {
            int heal;
            foreach (BattleObject healee in targetBattleObjectList) {
                if ((healee.currentHealth + healer.currentEnemyAbility.procHeal) <= healee.maxHealth) {
                    heal = Mathf.RoundToInt(healer.currentEnemyAbility.procHeal);
                }
                else {
                    heal = Mathf.RoundToInt(healee.maxHealth - healee.currentHealth);
                }
                healee.currentHealth += heal;
                healee.SpawnHealText(heal);
            }
        } //end HealProcMultiple


        //Effect functions

        public virtual void ApplyEffectSingle(Effect effect, BattleObject target) {
            effect.CreateEffectSingle(target);
        }

        public virtual void ApplyEffectMultiple(Effect effect) {
            effect.CreateEffectMultiple(targetBattleObjectList);
        }
        

    } //end EnemyAbility class

} //end EnemyAbilities namespace