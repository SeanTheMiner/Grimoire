using UnityEngine;
using System.Collections;
using Abilities;
using Effects;
using Procs;

public class ProcStorm : HeroAbility {
    
    public DamageProc primaryDamageProc = new DamageProc();
    public DamageProc secondaryDamageProc = new DamageProc();

    public EffectProc primaryEffectProc = new EffectProc();
    public EffectProc secondaryEffectProc = new EffectProc();
    

    public ProcStorm () {

        abilityName = "Proc Storm";
        abilityType = AbilityType.Barrage;
        targetScope = TargetScope.SingleEnemy;
        manaCost = 100;

        chargeDuration = 1;
        abilityDuration = 10;
        cooldownDuration = 5;
        
        primaryDamageProc.procDamage = 30;
        primaryDamageProc.procSpacing = 1;
        primaryDamageProc.damageType = DamageProc.DamageType.Physical;
        
        secondaryDamageProc.procDamage = 30;
        secondaryDamageProc.procSpacing = 1;
        secondaryDamageProc.procStartDelay = 0.5f;
        secondaryDamageProc.damageType = DamageProc.DamageType.Magical;
        secondaryDamageProc.dependentProc = primaryEffectProc;

        primaryEffectProc.effectApplied = new ArmorBreak();
        
        
    } //end Constructor()


    public override void AbilityMap() {

        if (nextProcTimer <= Time.time) {
            CheckTarget();
            DetermineHitOutcomeSingle(abilityOwner, targetEnemy);
            nextProcTimer = Time.time + procSpacing;
        }

        if (abilityEndTimer <= Time.time) {
            ExitAbility();
        }

    } //end AbilityMap()






} //end ProcStorm class
