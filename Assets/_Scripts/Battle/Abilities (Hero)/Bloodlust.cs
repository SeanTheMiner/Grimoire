using UnityEngine;
using System.Collections;
using Effects;
using Procs;

public class Bloodlust : HeroAbility {

    public HealProc healProc = new HealProc();
    public EffectProc effectProc = new EffectProc();
    
    public Bloodlust() {

        abilityName = "Bloodlust";

        abilityType = AbilityType.Burst;
        targetScope = TargetScope.SingleHero;
        abilityDamageType = AbilityDamageType.None;
        
        manaCost = 90;

        chargeDuration = 2.5f;
        cooldownDuration = 17;

        healProc.procHeal = 200;
        effectProc.effectApplied = new BloodlustEff();

    } //end Constructor()


    public override void AbilityMap() {

        CheckTarget();
        healProc.HealProcSingle(abilityOwner, targetHero);
        effectProc.ApplyEffectSingle(effectProc.effectApplied, targetHero);
        ExitAbility();

    } //end AbilityMap()


} //end Bloodlust class
