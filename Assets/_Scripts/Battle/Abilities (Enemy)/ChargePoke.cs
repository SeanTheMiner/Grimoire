using UnityEngine;
using System.Collections;
using EnemyAbilities;

public class ChargePoke : EnemyAbility {

    public ChargePoke() {
        
        abilityName = "Charge Poke";
        chargeDuration = 3.0f;
        procDamage = 40.0f;
        enemyAbilityWeight = 60;

        critChance = 20;
        critMultiplier = 1.5f;

        primaryDamageType = DamageType.Physical;
        targetScope = TargetScope.SingleHero;
        enemyAbilityType = EnemyAbilityType.Burst;

    } //end Constructor


    public override void EnemyAbilityMap() {

        DetermineHitOutcomeSingle(abilityOwner, targetHero);
        ExitEnemyAbility();

    } //end AbilityMap()


} //end Charge Poke class
