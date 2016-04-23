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