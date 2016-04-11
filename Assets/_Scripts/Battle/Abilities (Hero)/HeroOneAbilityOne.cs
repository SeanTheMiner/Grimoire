using UnityEngine;
using System.Collections;
using Abilities;
using Procs;

public class HeroOneAbilityOne : HeroAbility {

    public DamageProc soleProc = new DamageProc();

    public HeroOneAbilityOne() {
        
        abilityName = "Charge Punch";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;
        manaCost = 80;

        chargeDuration = 4.0f;
        cooldownDuration = 10.0f;
        procDamage = 250.0f;

        soleProc.damageType = DamageProc.DamageType.Physical;
        soleProc.procDamage = 250;
        soleProc.critChance = 50;
        soleProc.critMultiplier = 1.5f;
        
    } //end constructor
    

    public override void AbilityMap() {

        CheckTarget();
        DetermineHitOutcomeSingle(abilityOwner, targetEnemy, soleProc);
        ExitAbility();

    } //end AbilityType


} //end class
