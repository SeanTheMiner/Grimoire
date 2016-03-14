using UnityEngine;
using System.Collections;
using Abilities;

public class HeroOneAbilityOne : Ability {

    public HeroOneAbilityOne() {
        
        abilityName = "Charge Punch";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

        chargeDuration = 2.0f;
        cooldownDuration = 4.0f;
        procDamage = 170.0f;
        
    } //end constructor


    public override void AbilityMap() {

        CheckTarget();
        DetermineHitOutcomeSingle(abilityOwner, targetEnemy);
        ExitAbility();

    } //end AbilityType


} //end class
