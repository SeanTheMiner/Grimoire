using UnityEngine;
using System.Collections;
using Abilities;

public class HeroOneAbilityTwo : Ability {

    public HeroOneAbilityTwo() {

        abilityName = "Punch Barrage";
        abilityType = AbilityType.Barrage;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

        chargeDuration = 3.0f;
        abilityDuration = 6.0f;
        cooldownDuration = 4.0f;

        procDamage = 60;
        procSpacing = 0.5f;
        critChance = 30;
        critMultiplier = 2.5f;
        
    } //end constructor


    public override void AbilityMap() {
      
        if(nextProcTimer <= Time.time) {
            CheckTarget();
            DetermineHitOutcomeSingle(abilityOwner, targetEnemy);
            nextProcTimer = Time.time + procSpacing;
        }

        if(abilityEndTimer <= Time.time) {
            ExitAbility();
        }
        
    } //end AbilityMap()

} //end HeroOneAbilityTwo class