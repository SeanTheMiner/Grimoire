using UnityEngine;
using System.Collections;
using Abilities;

public class FireStorm : HeroAbility {

    public FireStorm() {

        abilityName = "Fire Storm";
        abilityType = AbilityType.InfBarrage;
        targetScope = TargetScope.Untargeted;
        primaryDamageType = DamageType.Magical;
        costsMana = false;

        chargeDuration = 2;
        procDamage = 30;
        procSpacing = 0.35f;

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