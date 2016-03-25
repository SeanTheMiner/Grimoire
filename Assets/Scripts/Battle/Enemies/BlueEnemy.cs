using UnityEngine;
using System.Collections;

using Enemies;

public class BlueEnemy : Enemy {

    public BlueEnemy ()
    {
        maxHealth = 1200;
        healthRegen = 2;
        homeBiome = Biome.BlueBiome;
        armor = 70;
        spirit = 20;
        physicalEvasionChance = 12;
        physicalBlockChance = 20;
        physicalBlockModifier = 40;

        enemyAbilityList.Add(enemyAbilityOne);
        enemyAbilityList.Add(enemyAbilityTwo);
        enemyAbilityList.Add(enemyAbilityTwo);
        
    }

}
