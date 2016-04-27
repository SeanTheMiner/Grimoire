using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using BattleObjects;
using Heroes;
using Enemies;
using Effects;
using Procs;


namespace Abilities {

    public abstract class Ability {

        public TargetingManager targetingManager = new TargetingManager();
       
        public string abilityName {
            get; set;
        }

        //eventually delete this, since it'll be on a proc
        public Effect effectApplied;

        public Proc primaryProc;
        
        public int effectStacksApplied {
            get; set;
        }

        public AbilityDamageType abilityDamageType {
            get; set;
        }
        
        public enum AbilityDamageType {
            Physical,
            Magical,
            Healing,
            None
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


        //Functions shared between Hero & Enemy Abilities

        public virtual void DetermineHitOutcomeSingle(BattleObject attacker, BattleObject defender, DamageProc damageProc) {

            HitManager.HitOutcome hitOutcome = HitManager.DetermineEvasionAndBlock(attacker, defender, this, damageProc);

            if (hitOutcome == HitManager.HitOutcome.Evade) {
                defender.SpawnMissText(damageProc.damageType);
                GameObject.Find("EventManager").GetComponent<EventManager>().
                    CheckForTriggers(attacker, defender, ProcTriggers.ProcTrigger.TriggerType.Evasion, damageProc.damageType);
                return;
            }
            if (hitOutcome == HitManager.HitOutcome.Block) {
                damageProc.ApplyBlockDamageProc(attacker, defender);
                GameObject.Find("EventManager").GetComponent<EventManager>().
                    CheckForTriggers(attacker, defender, ProcTriggers.ProcTrigger.TriggerType.Block, damageProc.damageType);
                return;
            }

            hitOutcome = HitManager.DetermineCrit(attacker, defender, damageProc);

            if (hitOutcome == HitManager.HitOutcome.Crit) {
                damageProc.ApplyCritDamageProc(attacker, defender);
                GameObject.Find("EventManager").GetComponent<EventManager>().
                    CheckForTriggers(attacker, defender, ProcTriggers.ProcTrigger.TriggerType.Crit, damageProc.damageType);
                return;
            }
            else {
                damageProc.ApplyDamageProc(attacker, defender);
                GameObject.Find("EventManager").GetComponent<EventManager>().
                    CheckForTriggers(attacker, defender, ProcTriggers.ProcTrigger.TriggerType.Damage, damageProc.damageType);
                return;
            }

        } //End DetermineHitOutComeSingle (3)


        public virtual void DetermineHitOutcomeMultiple(BattleObject attacker, List<BattleObject> targetList, DamageProc damageProc) {

            foreach (BattleObject defender in targetList) {
                DetermineHitOutcomeSingle(attacker, defender, damageProc);
            } //end foreach

        } //end DamageProcMultiple()
        

    } //end Ability class


} //end Abilities namespace