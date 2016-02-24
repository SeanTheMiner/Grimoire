using UnityEngine;
using System.Collections.Generic;
using Heroes;
using Abilities;

public class HeroOne : Hero {

    public HeroOne() {

        heroName = "Punchie McGee";
        maxHealth = 800;
        healthRegen = 10;
        armor = 50;
        spirit = 20;

        abilityOne = new HeroOneAbilityOne();
        abilityTwo = new HeroOneAbilityTwo();

    }
	
}
