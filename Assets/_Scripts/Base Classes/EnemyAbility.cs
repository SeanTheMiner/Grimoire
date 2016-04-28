using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Abilities;
using BattleObjects;
using Heroes;
using Enemies;
using Effects;


namespace EnemyAbilities {
    
    public class EnemyAbility : Ability {

        public Enemy abilityOwner;

        public int enemyAbilityWeight {
            get; set;
        }
        
        public int enemyAbilityWeightMark {
            get; set;
        }

        public bool requiresCharge {
            get; set;
        }

        public bool hasCooldown {
            get; set;
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
                targetEnemy = targetingManager.TargetRandomEnemy();
            }
            else if ((targetScope == TargetScope.SingleHero) && (targetHero == null) && (!targetHero.isDead)) {
                targetHero = targetingManager.TargetRandomHero();
            }

        } //end CheckTarget()


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


    } //end EnemyAbility class

} //end EnemyAbilities namespace