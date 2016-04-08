using UnityEngine;
using System.Collections;

using Abilities;
using Effects;

public class ArmorBreakAbility : HeroAbility {

    public ArmorBreakAbility () {

        abilityName = "Armor Breaker";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;
        manaCost = 80;

        chargeDuration = 4;
        cooldownDuration = 8;
        procDamage = 100;

        effectApplied = new ArmorBreak();
    }

 
    public override void AbilityMap() {
        CheckTarget();
        ApplyEffectSingle(effectApplied, targetEnemy);
        DetermineHitOutcomeSingle(abilityOwner, targetEnemy);
        ExitAbility();
    }


} //end ArmorBreakAbility class
