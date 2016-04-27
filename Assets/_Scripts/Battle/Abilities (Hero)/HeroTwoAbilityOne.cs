using UnityEngine;
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
        abilityDamageType = AbilityDamageType.Magical;
        manaCost = 100;
        
        requiresTargeting = false;
        appliesCoreEffect = true;

        chargeDuration = 2;
        cooldownDuration = 10.0f;

        damageProc.damageType = DamageProc.DamageType.Magical;
        damageProc.procDamage = 150.0f;

        effectProc.stacksApplied = 30;
        
    } //end Constructor()


    public override void SetCoreEffectApplied() {
        effectProc.effectApplied = coreEffectApplied;
    }


    public override void AbilityMap() {
        
        DetermineHitOutcomeMultiple(abilityOwner, targetingManager.TargetAllEnemies(), damageProc);
        effectProc.ApplyEffectMultiple(effectProc.effectApplied, targetingManager.TargetAllEnemies());
        ExitAbility();

    } //end AbilityMap()
    

} //end Ability