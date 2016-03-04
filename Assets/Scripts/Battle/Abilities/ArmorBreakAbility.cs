using UnityEngine;
using System.Collections;

using Abilities;

public class ArmorBreakAbility : Ability {

    public ArmorBreakAbility () {

        abilityName = "Armor Breaker";
        chargeDuration = 3;
        cooldownDuration = 5;
        procDamage = 100;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

    }

    public override void AbilityMap() {
        DamageProc(abilityOwner, targetEnemy);
        ApplyEffect(effectApplied, targetEnemy);
        ExitAbility();
    }

    public override void SetBattleState() {
        abilityOwner.currentBattleState = Heroes.Hero.BattleState.Burst;
    }

    public override void ClearTargeting() {
        targetEnemy = null;
    }

} //end ArmorBreakAbility class
