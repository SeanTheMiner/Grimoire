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

        abilityOne = new FireStorm();
        abilityTwo = new EndlessFire();
        abilityThree = new HeroTwoAbilityOne();
        abilityFour = new PiercingFire();
        abilityFive = new SetAblaze();
        abilitySix = new RingOfFire();

        SetAbilityOwner();

        //ON ability itself
        //abilityFour.effectApplied = new SpiritBreak();
        //abilityFive.effectApplied = new FlameStackEffect();
        //abilitySix.effectApplied = new FlameStackEffect();

    } //end Constructor()

} //end HeroTwo class