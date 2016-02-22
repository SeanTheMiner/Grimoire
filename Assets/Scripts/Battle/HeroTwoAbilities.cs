using UnityEngine;
using System.Collections;
using Abilities;

public class HeroTwoAbilityOne : Ability {

    public HeroTwoAbilityOne() {

        name = "Charge Blast";
        chargeDuration = 4.0f;
        cooldown = 4.0f;
        procDamage = 40.0f;
        targetScope = TargetScope.AllEnemies;
        primaryDamageType = DamageType.Magical;

    }
}

public class HeroTwoAbilityTwo : Ability {

    public HeroTwoAbilityTwo() {

        name = "ChargeHeal";
        chargeDuration = 4.0f;
        cooldown = 6.0f;
        procHeal = 100.0f;
        targetScope = TargetScope.AllHeroes;
        primaryDamageType = DamageType.Healing;

    }
}