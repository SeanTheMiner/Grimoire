using UnityEngine;
using System.Collections;
using EnemyAbilities;
using Procs;

public class HealPoke : EnemyAbility {

    public HealProc healProc = new HealProc();

    public HealPoke() {

        abilityName = "Heal Poke";
        chargeDuration = 5.0f;
        
        enemyAbilityWeight = 10;

        abilityDamageType = AbilityDamageType.Healing;
        targetScope = TargetScope.AllEnemies;
        enemyAbilityType = EnemyAbilityType.Burst;

        healProc.procHeal = 50;

    } //end Constructor


    public override void EnemyAbilityMap() {
        
        healProc.HealProcMultiple(abilityOwner, targetingManager.TargetAllEnemies());
        ExitEnemyAbility();

    } //end AbilityMap()


} //end Charge Poke class
