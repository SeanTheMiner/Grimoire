using UnityEngine;
using System.Collections;
using Abilities;

public class HeroTwoAbilityOne : Ability {

    public HeroTwoAbilityOne() {

        abilityName = "Charge Blast";
        chargeDuration = 4.0f;
        cooldownDuration = 4.0f;
        procDamage = 40.0f;
        targetScope = TargetScope.AllEnemies;
        primaryDamageType = DamageType.Magical;

    }
}