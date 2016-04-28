using UnityEngine;
using System.Collections;
using Abilities;
using Procs;
using Enemies;

public class FireStorm : HeroAbility {

    public DamageProc damageProc = new DamageProc();
    public EffectProc effectProc = new EffectProc();

    public FireStorm() {

        abilityName = "Fire Storm";

        abilityType = AbilityType.InfBarrage;
        targetScope = TargetScope.Untargeted;
        abilityDamageType = AbilityDamageType.Magical;

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

        UpdateInfBarrageMask(damageProc.nextProcTimer, damageProc.procSpacing);

        if (damageProc.nextProcTimer <= Time.time) {
            targetEnemy = targetingManager.TargetRandomEnemy();
            DetermineHitOutcomeSingle(abilityOwner, targetingManager.TargetRandomEnemy(), damageProc);
            effectProc.ApplyEffectSingle(effectProc.effectApplied, targetEnemy);
            damageProc.nextProcTimer = ApplySpacing(damageProc.procSpacing);
        }
       
    } //end Ability Map()
    
} //end FireStorm class