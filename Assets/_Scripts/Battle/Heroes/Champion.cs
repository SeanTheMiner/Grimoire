using UnityEngine;
using System.Collections.Generic;
using Heroes;
using Abilities;

public class Champion : Hero {

    public HeroManager heroManager;

    public enum ChampionStance {
        Offensive,
        Defensive
    }

    public ChampionStance currentStance;

    public override void BattleStart() {
        heroManager = GameObject.Find("HeroManager").GetComponent<HeroManager>();
        heroManager.activeChampion = this;
    }

    public Champion() {

        heroName = "The Best Around";

        maxHealth = 1000;
        maxMana = 500;
        healthRegen = 4;
        manaRegen = 10;

        armor = 90;
        spirit = 60;

        physicalBlockChance = 50;
        physicalBlockModifier = 50;
        magicalBlockChance = 30;
        magicalBlockModifier = 40;

        currentStance = ChampionStance.Defensive;

        abilityOne = new StandardSlashing();
        abilityTwo= new ChangeStance();
        abilityThree = new SwordBarrage();
        abilityFour = new Inspire();
        abilityFive = new Eviscerate();
        abilitySix = new ArmorBreakAbility();

        SetAbilityOwner();

        //eventually SetOwnerChampion just calls all 6, when they are all ChampionAbilities
        SetOwnerChampion((ChampionAbility)abilityOne);
        SetOwnerChampion((ChampionAbility)abilityTwo);
        SetOwnerChampion((ChampionAbility)abilityThree);
        SetOwnerChampion((ChampionAbility)abilityFour);
        SetOwnerChampion((ChampionAbility)abilityFive);

    } //end Constructor()


    public void ChangeStanceToOffensive () {

        currentStance = ChampionStance.Offensive;

        physicalBlockChance = 40;
        physicalBlockModifier = 40;
        magicalBlockChance = 30;
        magicalBlockModifier = 30;

        //Addeffect countery bit?
    } //end ChangeStanceToOffensive()


    public void ChangeStanceToDefensive () {

        currentStance = ChampionStance.Defensive;

        physicalBlockChance = 60;
        physicalBlockModifier = 60;
        magicalBlockChance = 40;
        magicalBlockModifier = 40;

        //removeeffect, or idk if this needs any effect
    } //end ChangeStanceToDefensive()
    
    
    private void SetOwnerChampion(ChampionAbility ability) {
        ability.ownerChampion = this;
    }


} //end Champion class