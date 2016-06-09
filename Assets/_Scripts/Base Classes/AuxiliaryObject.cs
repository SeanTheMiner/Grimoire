using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using BattleObjects;
using Procs;
using Abilities;


namespace AuxiliaryObjects {

    public class AuxiliaryObject : BattleObject {

        public TargetingManager targetingManager = new TargetingManager();

        public Ability sourceAbility;

        public bool enemyCanTarget {
            get; set;
        }

        public bool heroCanTarget {
            get; set;
        }

        public bool hasDuration {
            get; set;
        }

        public bool canMultiply {
            get; set;
        }

        public bool needsController {
            get; set;
        }
            


        public float objectDuration {
            get; set;
        }

        public float durationStartTimer {
            get; set;
        }

        public string auxiliaryObjectPrefabLocation;

        public AuxiliaryObject () {

            enemyCanTarget = false;
            heroCanTarget = false;
            hasDuration = true;
            canMultiply = false;
            needsController = false;

            
        } //End Constructor()


        public virtual void InitAuxiliaryObject() {

            if (hasDuration) {
                Destroy(gameObject, objectDuration);
            }
            
        } //End InitAuxiliaryObject()




        public virtual void DetermineHitOutcomeSingleAuxiliary(BattleObject defender, DamageProc damageProc) {

            HitManager.HitOutcome hitOutcome = HitManager.DetermineEvasionAndBlockAuxiliary(this, defender, damageProc);

            if (hitOutcome == HitManager.HitOutcome.Evade) {
                defender.SpawnMissText(damageProc.damageType);
                GameObject.Find("EventManager").GetComponent<EventManager>().
                    CheckForTriggers(null, defender, ProcTriggers.ProcTrigger.TriggerType.Evasion, damageProc.damageType);
                return;
            }
            if (hitOutcome == HitManager.HitOutcome.Block) {
                damageProc.ApplyBlockDamageProc(null, defender);
                GameObject.Find("EventManager").GetComponent<EventManager>().
                    CheckForTriggers(null, defender, ProcTriggers.ProcTrigger.TriggerType.Block, damageProc.damageType);
                return;
            }

            hitOutcome = HitManager.DetermineCritAuxiliary(this, defender, damageProc);

            if (hitOutcome == HitManager.HitOutcome.Crit) {
                damageProc.ApplyCritDamageProc(null, defender);
                GameObject.Find("EventManager").GetComponent<EventManager>().
                    CheckForTriggers(null, defender, ProcTriggers.ProcTrigger.TriggerType.Crit, damageProc.damageType);
                return;
            }
            else {
                damageProc.ApplyDamageProc(null, defender);
                GameObject.Find("EventManager").GetComponent<EventManager>().
                    CheckForTriggers(null, defender, ProcTriggers.ProcTrigger.TriggerType.Damage, damageProc.damageType);
                return;
            }

        }


    } //End AuxiliaryObject class
    
} //End AuxiliaryObjects namespace