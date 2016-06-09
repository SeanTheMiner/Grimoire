using UnityEngine;
using System.Collections;
using Heroes;

public class Myshka : Hero {

    public BlockAndHealEff blockAndHealEff = new BlockAndHealEff();

    public Myshka() {

        heroName = "Myshka";

        maxHealth = 720;
        healthRegen = 3;
        maxMana = 640;
        manaRegen = 22;
        
        armor = 10;
        spirit = 70;

        magicalBlockChance = 50;
        physicalEvasionChance = 30;

        abilityOne = new EbbAndFlow();
        abilityTwo = new HealBarrage();
        abilityThree = new ErodeArmor();
        abilityFour = new BlessWeapon();
        abilityFive = new Maelstrom();
        abilitySix = new LightningStorm();

        SetAbilityOwner();

    } //end Constructor()

    void Start () {
        blockAndHealEff.InitEffect(this);
    }


} //end Myshka class
