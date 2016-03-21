using UnityEngine;
using System.Collections.Generic;
using Heroes;
using Abilities;

public class HeroOne : Hero {

    public HeroOne() {

        heroName = "Punchie McGee";

        maxHealth = 1120;
        maxMana = 400;
        healthRegen = 5;
        manaRegen = 3;

        armor = 120;
        spirit = 40;

        physicalBlockChance = 25;
        physicalBlockModifier = 60;
        magicalBlockChance = 15;
        magicalBlockModifier = 40;

    }
    
}