using UnityEngine;
using System.Collections.Generic;

using Abilities;

namespace Heroes {

    public class Hero : MonoBehaviour {


        //Business time

        public string heroName {
            get; set;
        }

        public bool healthIsLocked {
            get; set;
        }

        public bool canTakeCommands {
            get; set;
        }


        //Stats
        
        public float maxHealth {
            get; set;
        }

        public float currentHealth {
            get; set;
        }

        public float healthRegen {
            get; set;
        }

        public float armor {
            get; set;
        }

        public float spirit {
            get; set;
        }


        //Under the hood

        public Ability selectedAbility;
        public Ability targetingAbility;
        public Ability currentAbility;
        public Ability defaultAbility;

        public Ability abilityOne;
        public Ability abilityTwo;
        public Ability abilityThree;

        //Eventually 1-6 go here!


        public BattleState currentBattleState {
            get; set;
        }

        public enum BattleState {
            Wait,
            Target,
            ReTarget,
            Charge,
            Burst,
            Barrage,
            InfCharge,
            InfBarrage,
            Uncharge,
            Dead
        }


        public Hero() {
            maxHealth = 0;
            currentHealth = 0;
            canTakeCommands = true;
            healthIsLocked = false;
            healthRegen = 0;
            armor = 0;
            spirit = 0;

        } //end constructor
    } //end Hero class
} //end Heroes namespace
