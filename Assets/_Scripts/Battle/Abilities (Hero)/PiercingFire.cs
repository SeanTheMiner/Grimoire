using UnityEngine;
using System.Collections;
using Abilities;
using Enemies;
using Procs;

public class PiercingFire : HeroAbility {

    public DamageProc damageProc = new DamageProc();
    public EffectProc effectProc = new EffectProc();

    public PiercingFire() {

        abilityName = "Piercing Fire";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.AllEnemies;
        primaryDamageType = DamageType.Magical;

        requiresTargeting = false;
        
        manaCost = 200;
        chargeDuration = 5.0f;
        cooldownDuration = 20;

        damageProc.procDamage = 100;
        damageProc.damageType = DamageProc.DamageType.Magical;

        effectProc.effectApplied = new SpiritBreak();

    } //end Constructor()

    
    public override void AbilityMap() {

        targetingManager.TargetAllEnemies(this);
        effectProc.ApplyEffectMultiple(effectProc.effectApplied, this);
        DetermineHitOutcomeMultiple(abilityOwner, damageProc);
        ExitAbility();

    } //end AbilityMap()


} //end Ability