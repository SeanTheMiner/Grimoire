using UnityEngine;
using System.Collections;
using Abilities;
using Enemies;

public class PiercingFire : Ability {

    public PiercingFire() {

        abilityName = "Piercing Fire";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.AllEnemies;
        primaryDamageType = DamageType.Magical;
        manaCost = 100;

        requiresTargeting = false;

        chargeDuration = 4.0f;
        cooldownDuration = 5.0f;
        procDamage = 80.0f;
        
    }

    public override void AbilityMap() {

        targetingManager.TargetAllEnemies(this);
        ApplyEffectMultiple(effectApplied);
        DetermineHitOutcomeMultiple(abilityOwner);
        ExitAbility();

    } //end AbilityMap()


} //end Ability