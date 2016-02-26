using UnityEngine;
using System.Collections;
using Abilities;

public class HeroOneAbilityOne : Ability {

    public HeroOneAbilityOne() {
        
        abilityName = "Charge Punch";
        chargeDuration = 2.0f;
        cooldownDuration = 4.0f;
        procDamage = 200.0f;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

    } //end constructor


    public override void AbilityMap() {
        DamageProc(abilityOwner, targetEnemy);
        ExitAbility();
    }

    public override void SetBattleState() {
        abilityOwner.currentBattleState = Heroes.Hero.BattleState.Burst;
    }

    public override void ClearTargeting() {
        targetEnemy = null;
    }
    
} //end class
