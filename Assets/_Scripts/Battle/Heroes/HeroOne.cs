using UnityEngine;
using System.Collections.Generic;
using Heroes;
using Abilities;
using Effects;

public class HeroOne : Hero {

    public EvadeCounterEff evadeCounterEff = new EvadeCounterEff();

    public HeroOne() {

        heroName = "Punchie McGee";

        maxHealth = 1120;
        maxMana = 400;
        healthRegen = 5;
        manaRegen = 3;

        armor = 120;
        spirit = 40;

        physicalEvasionChance = 50;

        physicalLifeSteal = 25;

        abilityOne = new EndlessPunches();
        abilityTwo = new MonkKata();
        abilityThree = new HeroOneAbilityTwo();
        abilityFour = new HeroOneAbilityOne();
        abilityFive = new RaiseSpirits();
        abilitySix = new ArmorBreakAbility();

        SetAbilityOwner();
        
    } //end Constructor()
    

    void Start () {
        evadeCounterEff.InitEffect(this);
    }
    
} //end HeroOne class