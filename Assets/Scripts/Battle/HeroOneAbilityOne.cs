using UnityEngine;
using System.Collections;
using Abilities;

public class HeroOneAbilityOne : Ability {

    public HeroOneAbilityOne() {

        abilityName = "Short Charge";
        chargeDuration = 2.0f;
        cooldown = 3.0f;
        procDamage = 60.0f;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

    }
}
