using UnityEngine;
using System.Collections;
using Abilities;

public class RingOfFire : HeroAbility {

    public RingOfFire() {

        abilityName = "Ring of Fire";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.FreeTargetAOE;
        primaryDamageType = DamageType.Magical;
        manaCost = 120;

        canBeDefault = false;

        chargeDuration = 4.0f;
        cooldownDuration = 12.0f;
        procDamage = 220.0f;
        radiusOfAOE = 4;

        effectStacksApplied = 8;

    } //end constructor


    public override void AbilityMap() {

        CheckAOETargets();
        DetermineHitOutcomeMultiple(abilityOwner);
        //ApplyEffectMultiple(effectApplied);
        ExitAbility();

    } //end AbilityType


} //end class
