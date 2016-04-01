using UnityEngine;
using System.Collections;
using Abilities;
using Enemies;

public class PiercingFire : HeroAbility {

    public PiercingFire() {

        abilityName = "Piercing Fire";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.AllEnemies;
        primaryDamageType = DamageType.Magical;
        manaCost = 200;

        requiresTargeting = false;

        chargeDuration = 5.0f;
        cooldownDuration = 20;
        procDamage = 100.0f;
        
    }

    public override void AbilityMap() {

        targetingManager.TargetAllEnemies(this);
        ApplyEffectMultiple(effectApplied);
        DetermineHitOutcomeMultiple(abilityOwner);
        ExitAbility();

    } //end AbilityMap()


} //end Ability