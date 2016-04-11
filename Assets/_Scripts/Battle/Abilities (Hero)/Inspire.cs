using UnityEngine;
using System.Collections;
using Procs;

public class Inspire : HeroAbility {

    public HealProc healProc = new HealProc();
    public EffectProc effectProc = new EffectProc();

    public Inspire() {

        abilityName = "Inspire";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.SingleHero;
        primaryDamageType = DamageType.Healing;
        manaCost = 100;
        
        chargeDuration = 3.0f;
        cooldownDuration = 17;
       
        healProc.procHeal = 220;

        effectProc.effectApplied = new InspireEffect();

    } //end Constructor()


    public override void AbilityMap() {

        CheckTarget();
        healProc.HealProcSingle(abilityOwner, targetHero);
        effectProc.ApplyEffectSingle(effectApplied, targetHero);
        ExitAbility();

    } //end AbilityMap()


} //end Ability