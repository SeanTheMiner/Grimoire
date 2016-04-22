using UnityEngine;
using System.Collections;

using Procs;

public class ArmorBreakAbility : HeroAbility {

    public DamageProc damageProc = new DamageProc();
    public EffectProc effectProc = new EffectProc();

    public ArmorBreakAbility () {

        abilityName = "Armor Breaker";

        abilityType = AbilityType.Burst;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

        manaCost = 80;
        chargeDuration = 4;
        cooldownDuration = 8;

        damageProc.procDamage = 100;
        damageProc.damageType = DamageProc.DamageType.Physical;

        effectProc.effectApplied = new ArmorBreak();

    } //end Constructor()

 
    public override void AbilityMap() {

        CheckTarget();
        effectProc.ApplyEffectSingle(effectProc.effectApplied, targetEnemy);
        DetermineHitOutcomeSingle(abilityOwner, targetEnemy, damageProc);
        ExitAbility();

    } //end AbilityMap()


} //end ArmorBreakAbility class
