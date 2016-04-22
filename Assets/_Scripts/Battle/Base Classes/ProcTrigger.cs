using UnityEngine;
using System.Collections;

using BattleObjects;
using Abilities;
using Effects;
using Procs;
using System.Collections.Generic;


namespace ProcTriggers {
    
    public class ProcTrigger {

        public string procTriggerName {
            get; set;
        }

        public TriggerType triggerType;
        public ObjectRole objectRole;
        public ProcScope procScope;

        public List<DamageProc.DamageType> approvedDamageTypeList = new List<DamageProc.DamageType>();

        public Proc procTriggered;
        public BattleObject triggerOwner;

        public enum TriggerType {
            Damage,
            Evasion,
            Block,
            Crit,
            Heal,
            Death
        }
        
        public enum ObjectRole {
            Actor,
            Receiver,
            ActorAlly,
            ReceiverAlly,
            SetActorAlly,
            SetReceiverAlly,
            Global
        }
        
        public enum ProcScope {
            Self,
            OtherParty,
            AllHeroes,
            AllEnemies
        }
        
    } //end ProcTrigger class

   
} //end ProcTriggers namespace




/*

   public class DamageTrigger : ProcTrigger {

       public DamageTriggerType damageTriggerType;

       public enum DamageTriggerType {
           Damaged,
           WasDamaged
       }

   } //end DamageTrigger class


   public class CritTrigger : ProcTrigger {

       public CritTriggerType critTriggerType;

       public enum CritTriggerType {
           Damaged,
           WasDamaged
       }

   } //end DamageTrigger class


   public class EvadeTrigger : ProcTrigger {

       public EvadeTriggerType evadeTriggerType;

       public enum EvadeTriggerType {
           Evaded,
           WasEvaded
       }

   } //end EvadeTrigger class


   public class BlockTrigger : ProcTrigger {

       public BlockTriggerType blockTriggerType;

       public enum BlockTriggerType {
           Blocked,
           WasBlocked
       }

   } //end BlockTrigger class



   public class HealTrigger : ProcTrigger {

       public HealTriggerType healTriggerType;

       public enum HealTriggerType {
           Healed,
           WasHealed
       }

   } //end HealTrigger class


   */
