using UnityEngine;
using System.Collections;
using Abilities;

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

        public virtual void SpawnDamageText(int damage, Ability.DamageType damageType) {

            GameObject damageTextPrefab = Instantiate(Resources.Load("DamageTextPrefab"),
                transform.position,
                Quaternion.identity
                ) as GameObject;
            
            
        } //end TakeDamage


    }
}
