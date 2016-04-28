using UnityEngine;
using System.Collections;
using EnemyAbilities;
using Procs;

public class PokeBarrage : EnemyAbility {

    public DamageProc damageProc = new DamageProc();

    public PokeBarrage() {

        abilityName = "Poke Barrage";

        abilityDamageType = AbilityDamageType.Physical;
        targetScope = TargetScope.SingleHero;
        enemyAbilityType = EnemyAbilityType.Barrage;
        
        chargeDuration = 4.0f;
        abilityDuration = 4;
        enemyAbilityWeight = 30;

        damageProc.procDamage = 20.0f;
        damageProc.procSpacing = 1;
        damageProc.damageType = DamageProc.DamageType.Physical;

    } //end Constructor


    public override void EnemyAbilityMap() {

        if (damageProc.nextProcTimer <= Time.time) {
            CheckTarget();
            DetermineHitOutcomeSingle(abilityOwner, targetHero, damageProc);
            damageProc.nextProcTimer = ApplySpacing(damageProc.procSpacing);
        }

        if (abilityEndTimer <= Time.time) {
            ExitEnemyAbility();
        }

    } //end AbilityMap()


} //end Charge Poke class
