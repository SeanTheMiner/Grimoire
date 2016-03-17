using UnityEngine;
using System.Collections;
using EnemyAbilities;

public class PokeBarrage : EnemyAbility {

    public PokeBarrage() {

        abilityName = "Poke Barrage";
        primaryDamageType = DamageType.Physical;
        targetScope = TargetScope.SingleHero;
        enemyAbilityType = EnemyAbilityType.Barrage;
        
        chargeDuration = 4.0f;
        procDamage = 20.0f;
        procSpacing = 1;
        abilityDuration = 4;
        enemyAbilityWeight = 30;
        
    } //end Constructor


    public override void EnemyAbilityMap() {

        if (nextProcTimer <= Time.time) {
            CheckTarget();
            DetermineHitOutcomeSingle(abilityOwner, targetHero);
            nextProcTimer = Time.time + procSpacing;
        }

        if (abilityEndTimer <= Time.time) {
            ExitEnemyAbility();
        }

    } //end AbilityMap()


} //end Charge Poke class
