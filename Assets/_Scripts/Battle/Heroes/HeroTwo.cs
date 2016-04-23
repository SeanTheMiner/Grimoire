using UnityEngine;
using System.Collections.Generic;
using Heroes;
using Effects;


public class HeroTwo : Hero {

    public HeroTwo() {

        heroName = "Fire Mage";

        maxHealth = 760;
        healthRegen = 3;
        maxMana = 550;
        manaRegen = 20;
        
        armor = 20;
        spirit = 100;

        magicalEvasionChance = 30;
        physicalEvasionChance = 20;

        magicalLifeSteal = 30;

        coreEffect = new FlameStackEffect();

        abilityOne = new FireStorm();
        abilityTwo = new EndlessFire();
        abilityThree = new HeroTwoAbilityOne();
        abilityFour = new PiercingFire();
        abilityFive = new SetAblaze();
        abilitySix = new RingOfFire();

        SetAbilityOwner();
        SetCoreEffects();
        
    } //end Constructor()

} //end HeroTwo class