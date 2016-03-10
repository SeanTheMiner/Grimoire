using UnityEngine;
using System.Collections;
using Abilities;
using Enemies;

public class HeroTwoAbilityOne : Ability {

    public HeroTwoAbilityOne() {

        abilityName = "Charge Blast";
        requiresTargeting = false;
        chargeDuration = 2.0f;
        cooldownDuration = 3.0f;
        procDamage = 100.0f;
        targetScope = TargetScope.AllEnemies;
        primaryDamageType = DamageType.Magical;

    }

    public override void AbilityMap() {
        DetermineHitOutcomeMultiple(abilityOwner);
        ExitAbility();
    } //end AbilityMap()
    
    public override void SetBattleState() {
        abilityOwner.currentBattleState = Heroes.Hero.BattleState.Burst;
    }

    public override void ClearTargeting() {
        targetBattleObjectList.Clear();
    }

} //end Ability