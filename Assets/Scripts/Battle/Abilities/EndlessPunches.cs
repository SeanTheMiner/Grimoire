using UnityEngine;
using System.Collections;
using Abilities;

public class EndlessPunches : Ability {

    public EndlessPunches() {

        abilityName = "Endless Punches";
        chargeDuration = 1.0f;
        isInfBarrage = true;
        hasCooldown = false;
        procDamage = 20.0f;
        procSpacing = 0.7f;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

    }


    public override void AbilityMap() {

        if(targetEnemy == null) {
            abilityOwner.currentBattleState = Heroes.Hero.BattleState.ReTarget;
        }

        if(nextProcTimer <= Time.time) {
            DamageProcSingle(abilityOwner, targetEnemy);
            nextProcTimer = Time.time + procSpacing;
        }
        
    } //end AbilityMap()

    public override void SetBattleState() {
        abilityOwner.currentBattleState = Heroes.Hero.BattleState.InfBarrage;
    }

    public override void ClearTargeting() {
        targetEnemy = null; 
    }

} //end EndlessPunches() (HAHAHA)
