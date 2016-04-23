using UnityEngine;
using System.Collections;
using Abilities;
using Procs;

public class HeroOneAbilityOne : HeroAbility {

    public DamageProc damageProc = new DamageProc();

    public HeroOneAbilityOne() {
        
        abilityName = "Charge Punch";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;
        manaCost = 80;

        chargeDuration = 4.0f;
        cooldownDuration = 10.0f;

        damageProc.damageType = DamageProc.DamageType.Physical;
        damageProc.procDamage = 250;
        damageProc.critChance = 50;
        damageProc.critMultiplier = 1.5f;
        
    } //end constructor
    

    public override void AbilityMap() {

        CheckTarget();
        DetermineHitOutcomeSingle(abilityOwner, targetEnemy, damageProc);
        ExitAbility();

    } //end AbilityType


} //end class
