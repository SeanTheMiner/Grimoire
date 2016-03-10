using UnityEngine;
using System.Collections;
using Abilities;
using Heroes;

public class HeroTwoAbilityTwo : Ability {

    public HeroTwoAbilityTwo() {

        abilityName = "Charge Heal";
        chargeDuration = 4.0f;
        cooldownDuration = 5.0f;
        procHeal = 120.0f;
        targetScope = TargetScope.AllHeroes;
        requiresTargeting = false;
        primaryDamageType = DamageType.Healing;

    }

    public override void AbilityMap() {

        HealProcMultiple(abilityOwner);
        ExitAbility();

    } //end AbilityMap()

    public override void SetBattleState() {
        abilityOwner.currentBattleState = Heroes.Hero.BattleState.Burst;
    }

    public override void ClearTargeting() {
        targetBattleObjectList.Clear();
    }

}