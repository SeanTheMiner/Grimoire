using UnityEngine;
using System.Collections;
using Abilities;

public class HeroOneAbilityTwo : Ability {

    public HeroOneAbilityTwo() {

        abilityName = "Punch Barrage";
        chargeDuration = 3.0f;
        abilityDuration = 10.0f;
        cooldownDuration = 5.0f;
        procDamage = 12.0f;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

    }
}