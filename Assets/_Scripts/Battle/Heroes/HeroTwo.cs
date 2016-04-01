using UnityEngine;
using System.Collections.Generic;
using Heroes;


public class HeroTwo : Hero {

    public HeroTwo() {

        heroName = "Weeetch";

        maxHealth = 760;
        healthRegen = 3;
        maxMana = 550;
        manaRegen = 5;


        armor = 20;
        spirit = 100;

        magicalEvasionChance = 30;
        physicalEvasionChance = 20;

    }

}