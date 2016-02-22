using UnityEngine;
using System.Collections;

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


    }
}
