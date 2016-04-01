using UnityEngine;
using System.Collections.Generic;
using Heroes;
using Abilities;

public class Champion : Hero {

    public enum ChampionStance {
        Offensive,
        Defensive
    }

    public ChampionStance currentStance;

    public Champion() {

        heroName = "The Best Around";

        maxHealth = 1000;
        maxMana = 500;
        healthRegen = 4;
        manaRegen = 4;

        armor = 90;
        spirit = 60;

        physicalBlockChance = 50;
        physicalBlockModifier = 50;
        magicalBlockChance = 30;
        magicalBlockModifier = 40;

        currentStance = ChampionStance.Defensive;

    }

    public void ChangeStanceToOffensive () {

        currentStance = ChampionStance.Offensive;

        physicalBlockChance = 40;
        physicalBlockModifier = 40;
        magicalBlockChance = 30;
        magicalBlockModifier = 30;

        //Addeffect countery bit?
    }

    public void ChangeStanceToDefensive () {

        currentStance = ChampionStance.Defensive;

        physicalBlockChance = 60;
        physicalBlockModifier = 60;
        magicalBlockChance = 40;
        magicalBlockModifier = 40;

        //removeeffect, or idk if this needs any effect
    }
    
} //end Champion