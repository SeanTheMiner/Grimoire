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
        
        manaCost = 100;
        chargeDuration = 5;
        cooldownDuration = 17;
        
        damageProc.procDamage = 120;
        damageProc.damageType = DamageProc.DamageType.Magical;

        effectProc.effectApplied = new FlameStackEffect();   
        effectProc.stacksApplied = 15;

    } //end Constructor()


    public override void AbilityMap() {

        CheckTarget();
        effectProc.ApplyEffectSingle(effectProc.effectApplied, targetEnemy);
        DetermineHitOutcomeSingle(abilityOwner, targetEnemy, damageProc);
        ExitAbility();

    } //end AbilityMap()


} //end ArmorBreakAbility class
