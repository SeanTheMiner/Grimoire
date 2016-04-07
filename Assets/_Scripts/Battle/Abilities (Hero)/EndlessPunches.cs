using UnityEngine;
using System.Collections;
using Abilities;

public class EndlessPunches : HeroAbility {

    public EndlessPunches() {

        abilityName = "Endless Punches";
        abilityType = AbilityType.InfBarrage;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;
        costsMana = false;

        chargeDuration = 2.0f;
        procDamage = 45;
        procSpacing = 0.7f;

        critChance = 30;
        critMultiplier = 2.5f;

        hasCooldown = false;

    }


    public override void AbilityMap() {

        if(nextProcTimer <= Time.time) {
            CheckTarget();
            DetermineHitOutcomeSingle(abilityOwner, targetEnemy);
            nextProcTimer = Time.time + procSpacing;
        }
        
    } //end AbilityMap()


} //end EndlessPunches() (HAHAHA)
