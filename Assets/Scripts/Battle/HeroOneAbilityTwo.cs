using UnityEngine;
using System.Collections;
using Abilities;

public class HeroOneAbilityTwo : Ability {

    public HeroOneAbilityTwo() {

        abilityName = "Long Charge";
        chargeDuration = 5.0f;
        cooldown = 5.0f;
        procDamage = 200.0f;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

    }
}