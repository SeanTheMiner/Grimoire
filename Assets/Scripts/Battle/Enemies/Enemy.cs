using UnityEngine;
using System.Collections;
using Abilities;
using DamageTextObjects;

namespace Enemies {

    public class Enemy : MonoBehaviour {

        public float maxHealth
        {
            get; set;
        }

        public float currentHealth
        {
            get; set;
        }
        

        public float healthRegen
        {
            get; set;
        }

        public int latestDamageTaken {
            get; set;
        }

        public Vector3 battlePosition {
            get; set;
        }

        public Biome homeBiome
        {
            get; set;
        }

        public enum Biome
        {
            RedBiome,
            BlueBiome
        }

        public Enemy ()
        {
            maxHealth = 0;
            currentHealth = 0;
            healthRegen = 0;

        }

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



    } //end Enemy class

} //end Enemies namespace