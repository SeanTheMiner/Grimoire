using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Heroes;
using Enemies;

namespace Abilities {

    public abstract class Ability : MonoBehaviour {

        //Name and hero

        public string abilityName {
            get; set;
        }

        public Hero abilityOwner;
            

        //Ability type-defining variables

        public bool requiresCharge {
            get; set;
        }

        public bool requiresTargeting {
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

        public List<Enemy> targetEnemyList = new List<Enemy>();
        public List<Hero> targetHeroList = new List<Hero>();

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


        //Other stuff


        public string resource {
            get; set;
        }

        public int cost {
            get; set;
        }

        

        //Virtual functions (to be overridden in each ability as needed)

        public virtual void AbilityMap() { }


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

        public virtual void SetBattleState() { }
        public virtual void ClearTargeting() { }

        public virtual void InitAbility() {
            abilityOwner.canTakeCommands = false;
            abilityEndTimer = Time.time + abilityDuration;
            SetBattleState();
        } //end InitAbility()


        public virtual void ExitAbility() {
            ClearTargeting();
            cooldownEndTimer = Time.time + cooldownDuration;
            abilityOwner.currentAbility = null;
            abilityOwner.canTakeCommands = true;
            abilityOwner.currentBattleState = Hero.BattleState.Wait;
        } //end ExitAbility()


        public virtual void DamageProc(Hero attacker, Enemy defender) {
            defender.currentHealth -= procDamage;
        }
        

        public virtual void HealProc(Hero healer, Hero healee) {
            healee.currentHealth += healer.currentAbility.procHeal;
            if(healee.currentHealth >= healee.maxHealth) {
                healee.currentHealth = healee.maxHealth;
            }
        }

      
        //Ability constructor

        public Ability() {

            abilityOwner = null;

            requiresCharge = true;
            requiresTargeting = true;
            
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

    } //end Ability class

} //end Ability namespace