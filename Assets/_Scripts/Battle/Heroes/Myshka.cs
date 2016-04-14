using UnityEngine;
using System.Collections;
using Heroes;

public class Myshka : Hero {

    public Myshka() {

        heroName = "Myshka";

        maxHealth = 720;
        healthRegen = 3;
        maxMana = 640;
        manaRegen = 6;


        armor = 10;
        spirit = 70;

        magicalEvasionChance = 60;
        physicalEvasionChance = 50;

        abilityOne = new EbbAndFlow();
        abilityTwo = new HealBarrage();
        abilityThree = new HeroTwoAbilityOne();
        abilityFour = new SetAblaze();
        abilityFive = new RingOfFire();
        abilitySix = new RaiseSpirits();

        SetAbilityOwner();

        //this needs to be put on the ability itself.
        //abilitySix.effectApplied = new RaiseSpiritsEff();

    } //end Constructor()


} //end Myshka class
