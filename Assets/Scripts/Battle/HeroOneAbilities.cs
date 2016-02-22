using UnityEngine;
using System.Collections;
using Abilities;

public class HeroOneAbilityOne : Ability {

    public HeroOneAbilityOne() {

        name = "Short Charge";
        chargeDuration = 2.0f;
        cooldown = 3.0f;
        procDamage = 60.0f;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

    }
}

public class HeroOneAbilityTwo : Ability {

    public HeroOneAbilityTwo() {

        name = "Long Charge";
        chargeDuration = 5.0f;
        cooldown = 5.0f;
        procDamage = 200.0f;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

    }
}