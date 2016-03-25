using UnityEngine;
using System.Collections;

using Enemies;

public class BlueEnemy : Enemy {

    public BlueEnemy ()
    {
        maxHealth = 2400;
        healthRegen = 3;
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
