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


        public virtual void SpawnDamageText(int damage) {

            GameObject damageTextPrefab = (GameObject)Instantiate(Resources.Load("DamageTextPrefab"),
                transform.position,
                Quaternion.Euler(90, 0, 0)
                );

            TextMesh damageTextMesh = damageTextPrefab.GetComponentInChildren<TextMesh>();
            damageTextMesh.text = damage.ToString();

        } //end SpawnDamageText()


        public virtual void SpawnHealText(int heal) {

            GameObject healTextPrefab = (GameObject)Instantiate(Resources.Load("HealTextPrefab"),
                transform.position,
                Quaternion.Euler(90, 0, 0)
                );

            TextMesh healTextMesh = healTextPrefab.GetComponentInChildren<TextMesh>();
            healTextMesh.text = heal.ToString();

        } //end SpawnHealText()
        
    } //end Hero class

} //end Heroes namespace
