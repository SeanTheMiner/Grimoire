using UnityEngine;
using System.Collections.Generic;
using Heroes;


public class HeroTwo : Hero {

    public HeroTwo() {

        name = "Weeetch";
        maxHealth = 600;
        healthRegen = 7;
        armor = 20;
        spirit = 50;

        abilityList.Add(new HeroTwoAbilityOne());
        abilityList.Add(new HeroTwoAbilityTwo());

    }

}
