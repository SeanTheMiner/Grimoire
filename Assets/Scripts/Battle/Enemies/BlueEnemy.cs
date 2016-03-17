using UnityEngine;
using System.Collections;

using Enemies;

public class BlueEnemy : Enemy {

    public BlueEnemy ()
    {
        maxHealth = 1200;
        healthRegen = 2;
        homeBiome = Biome.BlueBiome;
        armor = 80;
        spirit = 30;
        physicalEvasionChance = 10;
        physicalBlockChance = 20;
        physicalBlockModifier = 50;

        enemyAbilityList.Add(enemyAbilityOne);
        enemyAbilityList.Add(enemyAbilityTwo);
        enemyAbilityList.Add(enemyAbilityTwo);
        
    }

}
