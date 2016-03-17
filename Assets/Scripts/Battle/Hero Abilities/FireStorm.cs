using UnityEngine;
using System.Collections;
using Abilities;

public class FireStorm : Ability {

    public FireStorm() {

        abilityName = "Fire Storm";
        abilityType = AbilityType.InfBarrage;
        targetScope = TargetScope.Untargeted;
        primaryDamageType = DamageType.Magical;

        chargeDuration = 2;
        procDamage = 45;
        procSpacing = 0.4f;

        requiresTargeting = false;
        hasCooldown = false;

    }


    public override void AbilityMap() {

        if (nextProcTimer <= Time.time) {
            targetingManager.TargetRandomEnemy(this);
            DetermineHitOutcomeSingle(abilityOwner, targetEnemy);
            nextProcTimer = Time.time + procSpacing;
        }
       
    } //end Ability Map()
    
} //end FireStorm class