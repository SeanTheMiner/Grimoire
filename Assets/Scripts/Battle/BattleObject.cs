using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Effects;

namespace BattleObjects {

    //BattleObjects encompass Heroes and Enemies.
    //This way, if something treats both the same, you only have to write code once.

    public class BattleObject : MonoBehaviour {

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

        public List<Effect> effectList;
        

        
        //Native functions

        //eventually this guy will include (int damage, Ability.DamageType damageType)
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

        
    } //end BattleObject class

} //end namespace
