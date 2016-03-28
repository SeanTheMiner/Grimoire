using UnityEngine;
using System.Collections;
using Abilities;

public class HeroOneAbilityTwo : HeroAbility {

    public HeroOneAbilityTwo() {

        abilityName = "Punch Barrage";
        abilityType = AbilityType.Barrage;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;
        manaCost = 120;

        chargeDuration = 3.0f;
        abilityDuration = 5.0f;
        cooldownDuration = 12.0f;

        procDamage = 50;
        procSpacing = 0.5f;
        critChance = 25;
        critMultiplier = 3;
        
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