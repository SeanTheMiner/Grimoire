using UnityEngine;
using System.Collections.Generic;
using Heroes;
using Abilities;
using Effects;

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

        abilityOne = new EndlessPunches();
        abilityTwo = new MonkKata();
        abilityThree = new HeroOneAbilityTwo();
        abilityFour = new HeroOneAbilityOne();
        abilityFive = new Inspire();
        abilitySix = new ArmorBreakAbility();

        SetAbilityOwner();


        //this needs to be put on the ability itself.
            //abilityFive.effectApplied = new InspireEffect();
            //abilitySix.effectApplied = new ArmorBreak();

    } //end Constructor()
    

} //end HeroOne class