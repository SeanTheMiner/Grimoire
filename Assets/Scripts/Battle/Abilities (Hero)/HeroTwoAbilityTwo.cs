using UnityEngine;
using System.Collections;
using Abilities;
using Heroes;

public class HeroTwoAbilityTwo : HeroAbility {

    public HeroTwoAbilityTwo() {

        abilityName = "Charge Heal";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.AllHeroes;
        primaryDamageType = DamageType.Healing;
        requiresTargeting = false;
        manaCost = 85;
        
        chargeDuration = 4.0f;
        cooldownDuration = 10.0f;

        procHeal = 150.0f;
        
    } //end constructor

    public override void AbilityMap() {
        
        targetingManager.TargetAllHeroes(this);
        HealProcMultiple(abilityOwner);
        ExitAbility();

    } //end AbilityMap()
    

} //end HeroTwoAbilityTwo class