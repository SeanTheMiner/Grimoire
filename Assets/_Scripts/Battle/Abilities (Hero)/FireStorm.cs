using UnityEngine;
using System.Collections;
using Abilities;
using Procs;

public class FireStorm : HeroAbility {

    public DamageProc damageProc = new DamageProc();
    public EffectProc effectProc = new EffectProc();

    public FireStorm() {

        abilityName = "Fire Storm";

        abilityType = AbilityType.InfBarrage;
        targetScope = TargetScope.Untargeted;
        primaryDamageType = DamageType.Magical;

        costsMana = false;
        requiresTargeting = false;
        hasCooldown = false;
        appliesCoreEffect = true;

        chargeDuration = 2;
        
        damageProc.procDamage = 30;
        damageProc.damageType = DamageProc.DamageType.Magical;
        damageProc.procSpacing = 0.4f;
        
        effectProc.stacksApplied = 4;
        
    } //end Constructor()


    public override void SetCoreEffectApplied() {
        effectProc.effectApplied = coreEffectApplied;
    }


    public override void AbilityMap() {

        if (damageProc.nextProcTimer <= Time.time) {
            targetingManager.TargetRandomEnemy(this);
            DetermineHitOutcomeSingle(abilityOwner, targetEnemy, damageProc);
            effectProc.ApplyEffectSingle(effectProc.effectApplied, targetEnemy);
            damageProc.nextProcTimer = Time.time + damageProc.procSpacing;
        }
       
    } //end Ability Map()
    
} //end FireStorm class