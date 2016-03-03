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

        public virtual void SpawnDamageText() {

            Debug.Log(transform.position);

            GameObject damageTextPrefab = Instantiate(Resources.Load("DamageTextPrefab"),
                transform.position,
                Camera.main.transform.rotation
                ) as GameObject;

            TextMesh damageTextMesh = damageTextPrefab.GetComponent<TextMesh>();
            damageTextMesh.text = "Ouch!";

            //damageTextPrefab.damageDisplayed = (int)Mathf.Round(latestDamageTaken);
             
            
        } //end TakeDamage


    }
}
