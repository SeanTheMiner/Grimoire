using UnityEngine;
using System.Collections.Generic;
using Heroes;
using Abilities;
using Effects;

public class HeroOne : Hero {

    public EvadeCounterEff evadeCounterEff = new EvadeCounterEff();
    public ThickSkinEff thickSkinEff = new ThickSkinEff();

    public HeroOne() {

        heroName = "Punchie McGee";

        maxHealth = 1120;
        maxMana = 400;
        healthRegen = 5;
        manaRegen = 12;

        armor = 20;
        spirit = 50;

        physicalEvasionChance = 50;

        physicalLifeSteal = 12;

        abilityOne = new EndlessPunches();
        abilityTwo = new MonkKata();
        abilityThree = new HeroOneAbilityTwo();
        abilityFour = new HeroOneAbilityOne();
        abilityFive = new RaiseSpirits();
        abilitySix = new Bloodlust();

        SetAbilityOwner();
        
    } //end Constructor()
    

    void Start () {
        evadeCounterEff.InitEffect(this);
        thickSkinEff.InitEffect(this);
    }
    
} //end HeroOne class