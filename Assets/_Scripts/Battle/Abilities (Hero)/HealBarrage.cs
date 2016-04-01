using UnityEngine;
using System.Collections;

public class HealBarrage : HeroAbility {

    public HealBarrage() {

        abilityName = "Heal Barrage";
        abilityType = AbilityType.InfBarrage;
        targetScope = TargetScope.Untargeted;
        primaryDamageType = DamageType.Healing;
        costsMana = false;
        
        chargeDuration = 3;
        procHeal = 45;
        procSpacing = 0.8f;

        requiresTargeting = false;
        hasCooldown = false;

    }


    public override void AbilityMap() {

        if (nextProcTimer <= Time.time) {
            targetingManager.TargetRandomHero(this);
            HealProcSingle(abilityOwner, targetHero);
            nextProcTimer = Time.time + procSpacing;
        }

    } //end AbilityMap()


} //end HealBarrage class