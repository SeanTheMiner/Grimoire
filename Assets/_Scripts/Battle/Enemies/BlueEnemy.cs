using UnityEngine;
using System.Collections;

using Enemies;

public class BlueEnemy : Enemy {

    public BlueEnemy ()
    {
        maxHealth = 600;
        healthRegen = 3;
        homeBiome = Biome.BlueBiome;

        armor = 70;
        spirit = 20;

        physicalEvasionChance = 12;
        physicalBlockChance = 50;
        physicalBlockModifier = 40;
        
        enemyAbilityOne = new ChargePoke();
        enemyAbilityTwo = new PokeBarrage();
        enemyAbilityThree = new HealPoke();

        enemyAbilityList.Add(enemyAbilityOne);
        enemyAbilityList.Add(enemyAbilityTwo);
        enemyAbilityList.Add(enemyAbilityTwo);

        SetAbilityOwner();

    } //end Constructor()


} //end BlueEnemy class
