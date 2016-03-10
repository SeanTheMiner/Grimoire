using UnityEngine;
using System.Collections;
using Abilities;

public class HeroOneAbilityTwo : Ability {

    public HeroOneAbilityTwo() {

        abilityName = "Punch Barrage";
        chargeDuration = 3.0f;
        abilityDuration = 5.0f;
        cooldownDuration = 5.0f;
        procDamage = 30.0f;
        procSpacing = 0.5f;
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
            DamageProcSingle(abilityOwner, targetEnemy);
            nextProcTimer = Time.time + procSpacing;
        }

        if(abilityEndTimer <= Time.time) {
            ExitAbility();
        }
        
    } //end AbilityMap()

} //end HeroOneAbilityTwo class