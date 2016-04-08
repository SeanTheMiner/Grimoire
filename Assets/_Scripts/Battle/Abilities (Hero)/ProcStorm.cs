using UnityEngine;
using System.Collections;
using Abilities;
using Effects;

public class ProcStorm : HeroAbility {
    
    public DamageProc primaryDamageProc;
    public DamageProc secondaryDamageProc;
    public EffectProc effectProc;

    public Effect secondaryEffect;

    public ProcStorm () {

        abilityName = "Proc Storm";
        abilityType = AbilityType.Barrage;
        targetScope = TargetScope.SingleEnemy;
        manaCost = 100;

        chargeDuration = 1;
        abilityDuration = 10;
        cooldownDuration = 5;

        primaryDamageProc = gameObject.AddComponent<DamageProc>();
        primaryDamageProc.procDamage = 30;
        primaryDamageProc.procSpacing = 1;
        primaryDamageProc.damageType = Abilities.DamageProc.DamageType.Physical;

        secondaryDamageProc = gameObject.AddComponent<DamageProc>();
        secondaryDamageProc.procDamage = 30;
        secondaryDamageProc.procSpacing = 1;
        secondaryDamageProc.procStartDelay = 0.5f;
        secondaryDamageProc.damageType = Abilities.DamageProc.DamageType.Magical;
        secondaryDamageProc.dependentProc = effectProc;

        effectProc = gameObject.AddComponent<EffectProc>();
        effectProc.effectApplied = effectProc.gameObject.AddComponent<ArmorBreak>();
        
        
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
