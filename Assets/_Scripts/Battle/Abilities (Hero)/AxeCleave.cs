using UnityEngine;
using System.Collections;

using Procs;
using Effects;

public class AxeCleave : HeroAbility {

    public DamageProc damageProc = new DamageProc();
    public EffectProc effectProc = new EffectProc();

    public AxeCleave() {

        abilityName = "Axe Cleave";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.FreeTargetAOE;
        abilityDamageType = AbilityDamageType.Physical;
        manaCost = 100;

        canBeDefault = false;

        chargeDuration = 2;
        cooldownDuration = 10;
        radiusOfAOE = 7;

        damageProc.procDamage = 300;
        damageProc.damageType = DamageProc.DamageType.Physical;
        damageProc.critChance = .85f;
        damageProc.critMultiplier = 1.6f;
        damageProc.physicalPenetration = 90;

        effectProc.effectApplied = new AxeCleaveEff();

    } //End Constructor()


    public override void AbilityMap() {
        
        DetermineHitOutcomeMultiple(abilityOwner, CheckAOETargets(), damageProc);
        effectProc.ApplyEffectMultiple(effectProc.effectApplied, CheckAOETargets());
        ExitAbility();
	
    } //End AbilityMap()


} //End AxeCleave class