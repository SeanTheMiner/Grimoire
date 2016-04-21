using UnityEngine;
using System.Collections;

using BattleObjects;
using Abilities;
using Effects;
using Procs;


namespace ProcTriggers {


    public class ProcTrigger {

        public TriggerObject triggerObject;

        public enum TriggerObject {
            Self,
            SetHero,
            SetEnemy,
            AnyHero,
            AnyEnemy,
            Global
        }

        public DamageProc.DamageType damageTypeConstraint;

        public ProcScope procScope;

        public enum ProcScope {
            Self,
            OtherParty,
            AllHeroes,
            AllEnemies
        }
        
        public virtual bool CheckTrigger(BattleObject actor, BattleObject receiver, Proc proc) {
            return false;
        }

        public Proc procTriggered;
        public BattleObject triggerOwner;

    } //end ProcTrigger class



    public class DamageTrigger : ProcTrigger {

        public DamageTriggerType damageTriggerType;

        public enum DamageTriggerType {
            Damaged,
            WasDamaged
        }
        
        public virtual bool CheckTrigger(BattleObject actor, BattleObject receiver, DamageProc proc) {

            if ((triggerObject == TriggerObject.Self) && (actor == triggerOwner)) {
                ActivateTrigger(actor, receiver);
            }

            return false;
        }

        public virtual void ActivateTrigger(BattleObject triggerOwner, BattleObject procRecipient) {
            if (procTriggered is DamageProc) {
                (procTriggered as DamageProc).ApplyDamageProc(triggerOwner, procRecipient);
            }
        }
        

    } //end DamageTrigger class


    public class EvadeTrigger : ProcTrigger {

        public EvadeTriggerType evadeTriggerType;

        public enum EvadeTriggerType {
            Evaded,
            WasEvaded
        }

        public virtual void CheckTrigger(BattleObject attacker, BattleObject defender, DamageProc proc) {
           
            if (proc.damageType == damageTypeConstraint) {
                if (triggerObject == TriggerObject.Self) {
                    if ((evadeTriggerType == EvadeTriggerType.Evaded) && (defender == triggerOwner)) {
                        ActivateTrigger(defender, attacker);
                    }
                    else if ((evadeTriggerType == EvadeTriggerType.WasEvaded) && (attacker == triggerOwner)) {
                        ActivateTrigger(attacker, defender);
                    }
                } //end if self
            } //end if damage == damageTypeConstraint
            
        } //end CheckTrigger (2)


        public virtual void ActivateTrigger(BattleObject triggerOwner, BattleObject procRecipient) {
            if (procTriggered is DamageProc) {
                (procTriggered as DamageProc).ApplyDamageProc(triggerOwner, procRecipient);
            }
        } //end ActivateTrigger (2)
        
    } //end EvadeTrigger class


    public class BlockTrigger : ProcTrigger {

        public BlockTriggerType blockTriggerType;

        public enum BlockTriggerType {
            Blocked,
            WasBlocked
        }

        public virtual bool CheckTrigger(BattleObject Attacker, BattleObject Defender, DamageProc proc) {
            return false;
        }

        public virtual void ActivateTrigger(BattleObject triggerOwner, BattleObject procRecipient) {
            if (procTriggered is DamageProc) {
                (procTriggered as DamageProc).ApplyDamageProc(triggerOwner, procRecipient);
            }
            else if (procTriggered is EffectProc) {
                //(procTriggered as EffectProc).ApplyEffectSingle;
            }
        }

    } //end BlockTrigger class

    

    public class HealTrigger : ProcTrigger {

        public HealTriggerType healTriggerType;

        public enum HealTriggerType {
            Healed,
            WasHealed
        }
        
    } //end HealTrigger class
    

} //end ProcTriggers namespace