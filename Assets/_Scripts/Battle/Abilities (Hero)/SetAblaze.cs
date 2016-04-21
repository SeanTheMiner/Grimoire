using UnityEngine;
using System.Collections;

using Abilities;
using Effects;
using Procs;

public class SetAblaze : HeroAbility {

    public DamageProc damageProc = new DamageProc();
    public EffectProc effectProc = new EffectProc();

    //eventually effectProc dependent on damageProc

    public SetAblaze() {

        abilityName = "Set Ablaze";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Magical;

        appliesCoreEffect = true;

        manaCost = 10;
        chargeDuration = 2;
        cooldownDuration = 2;
        
        damageProc.procDamage = 120;
        damageProc.damageType = DamageProc.DamageType.Magical;
        
        effectProc.stacksApplied = 50;

    } //end Constructor()


    public override void SetCoreEffectApplied() {
        effectProc.effectApplied = coreEffectApplied;
    }


    public override void AbilityMap() {

        CheckTarget();
        effectProc.ApplyEffectSingle(effectProc.effectApplied, targetEnemy);
        DetermineHitOutcomeSingle(abilityOwner, targetEnemy, damageProc);
        ExitAbility();

    } //end AbilityMap()


} //end ArmorBreakAbility class
