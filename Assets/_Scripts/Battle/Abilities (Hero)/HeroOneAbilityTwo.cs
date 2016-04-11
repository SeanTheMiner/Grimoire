using UnityEngine;
using System.Collections;
using Abilities;
using Procs;

public class HeroOneAbilityTwo : HeroAbility {

    public DamageProc damageProc = new DamageProc();

    public HeroOneAbilityTwo() {

        abilityName = "Punch Barrage";
        abilityType = AbilityType.Barrage;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;
        manaCost = 120;

        chargeDuration = 3.0f;
        abilityDuration = 5.0f;
        cooldownDuration = 12.0f;

        damageProc.procDamage = 50;
        damageProc.procSpacing = 0.5f;
        damageProc.critChance = 25;
        damageProc.critMultiplier = 3;

    } //end constructor


    public override void AbilityMap() {
      
        if(damageProc.nextProcTimer <= Time.time) {
            CheckTarget();
            DetermineHitOutcomeSingle(abilityOwner, targetEnemy, damageProc);
            damageProc.nextProcTimer = Time.time + procSpacing;
        }

        if(abilityEndTimer <= Time.time) {
            ExitAbility();
        }
        
    } //end AbilityMap()

} //end HeroOneAbilityTwo class