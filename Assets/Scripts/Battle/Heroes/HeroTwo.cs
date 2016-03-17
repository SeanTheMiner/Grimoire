using UnityEngine;
using System.Collections.Generic;
using Heroes;


public class HeroTwo : Hero {

    public HeroTwo() {

        heroName = "Weeetch";
        maxHealth = 735;
        healthRegen = 2;
        armor = 20;
        spirit = 100;

        magicalEvasionChance = 30;
        physicalEvasionChance = 20;

    }

}