using UnityEngine;
using System.Collections;
using EnemyAbilities;

public class HealPoke : EnemyAbility {

    public HealPoke() {

        abilityName = "Heal Poke";
        chargeDuration = 5.0f;
        procHeal = 50.0f;
        enemyAbilityWeight = 10;

        primaryDamageType = DamageType.Healing;
        targetScope = TargetScope.AllEnemies;
        enemyAbilityType = EnemyAbilityType.Burst;

    } //end Constructor


    public override void EnemyAbilityMap() {

        targetingManager.EnemyTargetAllEnemies(this);
        HealProcMultiple(abilityOwner);
        ExitEnemyAbility();

    } //end AbilityMap()


} //end Charge Poke class
