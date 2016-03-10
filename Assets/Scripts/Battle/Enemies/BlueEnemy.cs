using UnityEngine;
using System.Collections;

using Enemies;

public class BlueEnemy : Enemy {

    public BlueEnemy ()
    {
        maxHealth = 1200;
        healthRegen = 2;
        homeBiome = Biome.BlueBiome;
        armor = 100;
        spirit = 50;
        physicalEvasionChance = 10;
        physicalBlockChance = 15;
        physicalBlockModifier = 40;
    }

}
