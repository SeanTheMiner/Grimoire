using UnityEngine;
using System.Collections;

using Enemies;

public class RedEnemy : Enemy {

    public RedEnemy()
    {
        maxHealth = 900;
        healthRegen = 7;
        homeBiome = Biome.BlueBiome;
    }
}
