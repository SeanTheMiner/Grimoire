using UnityEngine;
using System.Collections;
using Abilities;


public class HeroTwoAbilityOne : Ability {

    public HeroTwoAbilityOne() {

        abilityName = "Charge Blast";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.AllEnemies;
        primaryDamageType = DamageType.Magical;
        manaCost = 100;
        
        requiresTargeting = false;

        chargeDuration = 5.0f;
        cooldownDuration = 10.0f;
        procDamage = 150.0f;
       
    }

    public override void AbilityMap() {
        targetingManager.TargetAllEnemies(this);
        DetermineHitOutcomeMultiple(abilityOwner);
        ExitAbility();
    } //end AbilityMap()
    

} //end Ability