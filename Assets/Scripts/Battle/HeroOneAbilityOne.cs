using UnityEngine;
using System.Collections;
using Abilities;

public class HeroOneAbilityOne : Ability {

    public HeroOneAbilityOne() {
        
        abilityName = "Charge Punch";
        chargeDuration = 3.0f;
        cooldownDuration = 4.0f;
        procDamage = 150.0f;
        procLimit = 1;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

    } //end constructor


    public override void AbilityMap() {

        CheckCharge();
        if (hasCharged == false) {
            return;
        } 

        if (nextProcTimer <= Time.time) {
            Debug.Log("Owner:" + abilityOwner.heroName);
            Debug.Log("Target:" + targetEnemy.name);

            DamageProc(abilityOwner, targetEnemy);
            nextProcTimer = Time.time + procSpacing;
        }

        if ((abilityEndTimer <= Time.time) | (procCounter >= procLimit)) {
            ExitAbility();
        }

    } //end AbilityMap

} //end class
