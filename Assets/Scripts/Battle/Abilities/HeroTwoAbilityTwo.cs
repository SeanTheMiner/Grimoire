using UnityEngine;
using System.Collections;
using Abilities;
using Heroes;

public class HeroTwoAbilityTwo : Ability {

    public HeroTwoAbilityTwo() {

        abilityName = "Charge Heal";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.AllHeroes;
        primaryDamageType = DamageType.Healing;
        requiresTargeting = false;
        
        chargeDuration = 4.0f;
        cooldownDuration = 5.0f;

        procHeal = 120.0f;
        
    } //end constructor

    public override void AbilityMap() {
        //CheckTarget();
        //I think that's not necessary?
        HealProcMultiple(abilityOwner);
        ExitAbility();
    } //end AbilityMap()
    

} //end HeroTwoAbilityTwo class