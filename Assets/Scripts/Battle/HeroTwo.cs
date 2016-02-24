using UnityEngine;
using System.Collections.Generic;
using Heroes;


public class HeroTwo : Hero {

    public HeroTwo() {

        heroName = "Weeetch";
        maxHealth = 600;
        healthRegen = 7;
        armor = 20;
        spirit = 50;

        abilityOne = new HeroTwoAbilityOne();
        abilityTwo = new HeroTwoAbilityTwo();

    }

}
