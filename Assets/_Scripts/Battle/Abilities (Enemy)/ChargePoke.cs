using UnityEngine;
using System.Collections;
using EnemyAbilities;
using Procs;

public class ChargePoke : EnemyAbility {

    public DamageProc damageProc = new DamageProc();
    
    public ChargePoke() {
        
        abilityName = "Charge Poke";
        chargeDuration = 3.0f;
        enemyAbilityWeight = 60;

        damageProc.procDamage = 40.0f;
        damageProc.critChance = 20;
        damageProc.critMultiplier = 1.5f;
        damageProc.damageType = DamageProc.DamageType.Physical;

        abilityDamageType = AbilityDamageType.Physical;
        targetScope = TargetScope.SingleHero;
        enemyAbilityType = EnemyAbilityType.Burst;

    } //end Constructor()


    public override void EnemyAbilityMap() {

        DetermineHitOutcomeSingle(abilityOwner, targetHero, damageProc);
        ExitEnemyAbility();

    } //end AbilityMap()


} //end Charge Poke class
