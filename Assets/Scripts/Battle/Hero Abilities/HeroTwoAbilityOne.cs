using UnityEngine;
using System.Collections;
using Abilities;


public class HeroTwoAbilityOne : Ability {

    public HeroTwoAbilityOne() {

        abilityName = "Charge Blast";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.AllEnemies;
        primaryDamageType = DamageType.Magical;
        
        requiresTargeting = false;

        chargeDuration = 4.0f;
        cooldownDuration = 10.0f;
        procDamage = 300.0f;
       
    }

    public override void AbilityMap() {
        targetingManager.TargetAllEnemies(this);
        DetermineHitOutcomeMultiple(abilityOwner);
        ExitAbility();
    } //end AbilityMap()
    

} //end Ability