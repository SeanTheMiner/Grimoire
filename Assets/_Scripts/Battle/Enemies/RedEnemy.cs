using UnityEngine;
using System.Collections;

using Enemies;

public class RedEnemy : Enemy {

    public RedEnemy() {

        maxHealth = 500;
        healthRegen = 5;
        homeBiome = Biome.BlueBiome;

        armor = 30;
        spirit = 80;

        magicalEvasionChance = 20;
        magicalBlockChance = 10;
        magicalBlockModifier = 70;

        tenacity = 35;

        enemyAbilityOne = new ChargePoke();
        enemyAbilityTwo = new PokeBlast();
        enemyAbilityThree = new HealPoke();
        
        enemyAbilityList.Add(enemyAbilityOne);
        enemyAbilityList.Add(enemyAbilityTwo);
        enemyAbilityList.Add(enemyAbilityTwo);

        SetAbilityOwner();

    } //end Constructor()

    
} //end RedEnemy class