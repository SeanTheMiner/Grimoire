using UnityEngine;
using System.Collections;
using Abilities;

public class HeroTwoAbilityTwo : Ability {

    public HeroTwoAbilityTwo() {

        abilityName = "Charge Heal";
        chargeDuration = 4.0f;
        cooldown = 6.0f;
        procHeal = 100.0f;
        targetScope = TargetScope.AllHeroes;
        primaryDamageType = DamageType.Healing;

    }
}