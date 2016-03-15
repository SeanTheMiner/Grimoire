using UnityEngine;
using System.Collections.Generic;

using BattleObjects;
using Abilities;

namespace Heroes {

    public class Hero : BattleObject {


        public string heroName {
            get; set;
        }

        public bool healthIsLocked {
            get; set;
        }

        public bool canTakeCommands {
            get; set;
        }


        //any stats that aren't inherited from BattleObject go here. 
        //I don't know what they'd be other than mana.

        public float maxMana {
            get; set;
        }

        public float currentMana {
            get; set;
        }


        //Under the hood

        [HideInInspector]
        public Ability currentAbility, queuedAbility, selectedAbility, targetingAbility, defaultAbility;
        public Ability abilityOne, abilityTwo, abilityThree;
        
        public BattleState currentBattleState {
            get; set;
        }

        public enum BattleState {
            Wait,
            Target,
            Charge,
            InfCharge,
            InfBarrage,
            Ability,
            Uncharge,
            Dead
        }


        public Hero() {

            canTakeCommands = true;
            healthIsLocked = false;
            
        } //end constructor

        
    } //end Hero class

} //end Heroes namespace
