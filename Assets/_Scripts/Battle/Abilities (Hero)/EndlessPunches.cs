using UnityEngine;
using System.Collections;
using Abilities;
using Procs;

public class EndlessPunches : HeroAbility {

    public DamageProc damageProc = new DamageProc();

    public EndlessPunches() {

        abilityName = "Endless Punches";

        abilityType = AbilityType.InfBarrage;
        targetScope = TargetScope.SingleEnemy;
        abilityDamageType = AbilityDamageType.Physical;

        costsMana = false;
        hasCooldown = false;

        chargeDuration = 2.0f;
        
        damageProc.procDamage = 40;
        damageProc.procSpacing = 0.7f;
        damageProc.damageType = DamageProc.DamageType.Physical;
        damageProc.critChance = 20;
        damageProc.critMultiplier = 2.5f;
        damageProc.physicalFinesse = 40;
        
    } //end Constructor()


    public override void AbilityMap() {

        UpdateInfBarrageMask(damageProc.nextProcTimer, damageProc.procSpacing);

        if(damageProc.nextProcTimer <= Time.time) {
            CheckTarget();
            DetermineHitOutcomeSingle(abilityOwner, targetEnemy, damageProc);
            ApplySpacing(damageProc.nextProcTimer, damageProc.procSpacing);
        }
        
    } //end AbilityMap()


} //end EndlessPunches() (HAHAHA)
