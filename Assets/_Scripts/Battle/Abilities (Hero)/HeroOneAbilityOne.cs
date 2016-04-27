using UnityEngine;
using System.Collections;
using Abilities;
using Procs;

public class HeroOneAbilityOne : HeroAbility {

    public DamageProc damageProc = new DamageProc();

    public HeroOneAbilityOne() {
        
        abilityName = "Sapping Punch";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.SingleEnemy;
        abilityDamageType = AbilityDamageType.Physical;
        manaCost = 120;

        chargeDuration = 5.0f;
        cooldownDuration = 18;

        damageProc.damageType = DamageProc.DamageType.Physical;
        damageProc.procDamage = 300;
        damageProc.physicalAccuracy = 50;
        damageProc.physicalFinesse = 50;
        damageProc.physicalPenetration = 50;
        damageProc.lifeSteal = 50;
        
    } //end constructor
    

    public override void AbilityMap() {

        CheckTarget();
        DetermineHitOutcomeSingle(abilityOwner, targetEnemy, damageProc);
        ExitAbility();

    } //end AbilityType


} //end class
