using UnityEngine;
using System.Collections;
using Procs;

public class RaiseSpirits : HeroAbility {

    public HealProc healProc = new HealProc();
    public EffectProc effectProc = new EffectProc();
    
    public RaiseSpirits() {
        
        abilityName = "Raise Spirits";

        abilityType = AbilityType.Burst;
        targetScope = TargetScope.AllHeroes;
        primaryDamageType = DamageType.Healing;

        requiresTargeting = false;

        manaCost = 250;

        chargeDuration = 2.0f;
        cooldownDuration = 20;

        healProc.procHeal = 90;
        effectProc.effectApplied = new RaiseSpiritsEff();
        
    } //end Constructor()
    

    public override void AbilityMap() {

        targetingManager.TargetAllHeroes(this);
        effectProc.ApplyEffectMultiple(effectApplied, this);
        healProc.HealProcMultiple(abilityOwner, this);
        ExitAbility();

    } //end AbilityMap()


} //end Ability