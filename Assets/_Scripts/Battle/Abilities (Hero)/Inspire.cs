using UnityEngine;
using System.Collections;

public class Inspire : HeroAbility {

    public Inspire() {

        abilityName = "Inspire";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.SingleHero;
        primaryDamageType = DamageType.Healing;
        manaCost = 100;
        
        chargeDuration = 3.0f;
        cooldownDuration = 17;
        procHeal = 220;

        effectApplied = new InspireEffect();

    }


    public override void AbilityMap() {

        CheckTarget();
        HealProcSingle(abilityOwner, targetHero);
        ApplyEffectSingle(effectApplied, targetHero);
        ExitAbility();

    } //end AbilityMap()


} //end Ability