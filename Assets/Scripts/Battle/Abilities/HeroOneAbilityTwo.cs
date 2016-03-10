using UnityEngine;
using System.Collections;
using Abilities;

public class HeroOneAbilityTwo : Ability {

    public HeroOneAbilityTwo() {

        abilityName = "Punch Barrage";
        chargeDuration = 3.0f;
        abilityDuration = 6.0f;
        cooldownDuration = 5.0f;

        procDamage = 25.0f;
        procSpacing = 0.4f;
        critChance = 20;
        critMultiplier = 2;

        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

    }

    //Basic barrage for now

    public override void SetBattleState() {
        abilityOwner.currentBattleState = Heroes.Hero.BattleState.Barrage;
    }

    public override void AbilityMap() {
        base.AbilityMap();

        if(nextProcTimer <= Time.time) {
            DetermineHitOutcomeSingle(abilityOwner, targetEnemy);
            nextProcTimer = Time.time + procSpacing;
        }

        if(abilityEndTimer <= Time.time) {
            ExitAbility();
        }
        
    } //end AbilityMap()

} //end HeroOneAbilityTwo class