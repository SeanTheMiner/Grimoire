using UnityEngine;
using System.Collections.Generic;

using Abilities;

namespace Heroes {

    public class Hero : MonoBehaviour {

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

        public List<Ability> abilityList;

        public BattleState currentBattleState {
            get; set;
        }

        public enum BattleState {
            Null,
            Wait,
            Target,
            Charge,
            Burst,
            Barrage,
            InfBarrage,
            InfCharge,
            Uncharge,
            Dead
        }


        public Hero() {
            maxHealth = 0;
            currentHealth = 0;
            healthRegen = 0;
            armor = 0;
            spirit = 0;

        } //end constructor
    } //end Hero class
} //end Heroes namespace
