using UnityEngine;
using System.Collections;

using Abilities;
using Effects;

public class SetAblaze : HeroAbility {

    public SetAblaze() {

        abilityName = "Set Ablaze";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Magical;
        manaCost = 100;

        chargeDuration = 5;
        cooldownDuration = 17;
        procDamage = 120;
        
        effectStacksApplied = 20;

    }

    void Awake () {
        effectApplied = new FlameStackEffect();
    }


    public override void AbilityMap() {
        CheckTarget();
        ApplyEffectSingle(effectApplied, targetEnemy);
        DetermineHitOutcomeSingle(abilityOwner, targetEnemy);
        ExitAbility();
    }


} //end ArmorBreakAbility class
