using UnityEngine;
using System.Collections.Generic;
using Heroes;


public class HeroOne : Hero {

    public HeroOne() {

        name = "Punchie McGee";
        maxHealth = 800;
        healthRegen = 10;
        armor = 50;
        spirit = 20;

        abilityList.Add(new HeroOneAbilityOne());
        abilityList.Add(new HeroOneAbilityTwo());

    }
	
}
