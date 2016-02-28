using UnityEngine;
using System.Collections;
using Abilities;
using Enemies;

public class HeroTwoAbilityOne : Ability {

    public HeroTwoAbilityOne() {

        abilityName = "Charge Blast";
        requiresTargeting = false;
        chargeDuration = 2.0f;
        cooldownDuration = 2.0f;
        procDamage = 50.0f;
        targetScope = TargetScope.AllEnemies;
        primaryDamageType = DamageType.Magical;

    }

    public override void AbilityMap() {
        
        foreach (Enemy enemy in targetEnemyList) {
            DamageProc(abilityOwner, enemy);
        }
        ExitAbility();

    } //end AbilityMap()
    
    public override void SetBattleState() {
        abilityOwner.currentBattleState = Heroes.Hero.BattleState.Burst;
    }

    public override void ClearTargeting() {
        targetEnemyList.Clear();
    }

} //end Ability