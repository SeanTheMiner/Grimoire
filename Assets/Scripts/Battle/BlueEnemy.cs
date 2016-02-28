using UnityEngine;
using System.Collections;

using Enemies;

public class BlueEnemy : Enemy {

    public BlueEnemy ()
    {
        maxHealth = 1200;
        healthRegen = 3;
        homeBiome = Biome.BlueBiome;
    }

}
