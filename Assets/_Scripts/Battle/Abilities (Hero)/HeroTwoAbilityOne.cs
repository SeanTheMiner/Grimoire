﻿using UnityEngine;
using System.Collections;
using Abilities;
using Procs;


public class HeroTwoAbilityOne : HeroAbility {

    public DamageProc damageProc = new DamageProc();
    public EffectProc effectProc = new EffectProc();

    public HeroTwoAbilityOne() {

        abilityName = "Charge Blast";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.AllEnemies;
        primaryDamageType = DamageType.Magical;
        manaCost = 100;
        
        requiresTargeting = false;

        chargeDuration = 2;
        cooldownDuration = 10.0f;

        damageProc.damageType = DamageProc.DamageType.Magical;
        damageProc.procDamage = 150.0f;

        effectProc.effectApplied = new FlameStackEffect();
        effectProc.stacksApplied = 40;
       
    } //end Constructor()

    public override void AbilityMap() {

        targetingManager.TargetAllEnemies(this);
        DetermineHitOutcomeMultiple(abilityOwner, damageProc);
        effectProc.ApplyEffectMultiple(effectProc.effectApplied, this);
        ExitAbility();

    } //end AbilityMap()
    

} //end Ability