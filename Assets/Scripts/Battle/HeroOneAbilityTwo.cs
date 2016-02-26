using UnityEngine;
using System.Collections;
using Abilities;

public class HeroOneAbilityTwo : Ability {

    public HeroOneAbilityTwo() {

        abilityName = "Punch Barrage";
        chargeDuration = 3.0f;
        abilityDuration = 10.0f;
        cooldownDuration = 5.0f;
        procDamage = 12.0f;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

    }

    //Basic barrage for now

    public override void AbilityMap() {

        if(nextProcTimer <= Time.time) {
            Debug.Log("Owner:" + abilityOwner.heroName);
            Debug.Log("Target:" + targetEnemy.name);

            DamageProc(abilityOwner, targetEnemy);
            nextProcTimer = Time.time + procSpacing;
        }

        if((abilityEndTimer <= Time.time) | (procCounter >= procLimit)) {
            ExitAbility();
        }

    } //end AbilityMap


}