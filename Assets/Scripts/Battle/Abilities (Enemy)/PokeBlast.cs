using UnityEngine;
using System.Collections;
using EnemyAbilities;

public class PokeBlast : EnemyAbility {

    public PokeBlast() {

        abilityName = "Poke Blast";
        chargeDuration = 5.0f;
        procDamage = 40.0f;
        enemyAbilityWeight = 30;

        primaryDamageType = DamageType.Magical;
        targetScope = TargetScope.AllHeroes;
        enemyAbilityType = EnemyAbilityType.Burst;

    } //end Constructor


    public override void EnemyAbilityMap() {

        targetingManager.EnemyTargetAllHeroes(this);
        DetermineHitOutcomeMultiple(abilityOwner);
        ExitEnemyAbility();

    } //end AbilityMap()


} //end Charge Poke class
