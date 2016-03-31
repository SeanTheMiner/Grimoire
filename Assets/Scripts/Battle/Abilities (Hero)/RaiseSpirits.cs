using UnityEngine;
using System.Collections;

public class RaiseSpirits : HeroAbility {

    public RaiseSpirits() {

        abilityName = "Raise Spirits";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.AllHeroes;
        primaryDamageType = DamageType.Healing;
        manaCost = 250;

        requiresTargeting = false;

        chargeDuration = 5.0f;
        cooldownDuration = 20;
        procHeal = 100;
        
    }

    public override void AbilityMap() {

        targetingManager.TargetAllHeroes(this);
        ApplyEffectMultiple(effectApplied);
        HealProcMultiple(abilityOwner);
        ExitAbility();

    } //end AbilityMap()


} //end Ability