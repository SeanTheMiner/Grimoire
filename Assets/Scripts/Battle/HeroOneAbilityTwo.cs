﻿using UnityEngine;
using System.Collections;
using Abilities;

public class HeroOneAbilityTwo : Ability {

    public HeroOneAbilityTwo() {

        abilityName = "Punch Barrage";
        chargeDuration = 3.0f;
        abilityDuration = 5.0f;
        cooldownDuration = 5.0f;
        procDamage = 20.0f;
        procSpacing = 0.5f;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

    }

    //Basic barrage for now

    public override void InitAbility() {
        base.InitAbility();
        SetBattleState();
    }

    public override void SetBattleState() {
        abilityOwner.currentBattleState = Heroes.Hero.BattleState.Barrage;
    }

    public override void AbilityMap() {

        if (targetEnemy == null) {
            abilityOwner.currentBattleState = Heroes.Hero.BattleState.ReTarget;
        }

        if(nextProcTimer <= Time.time) {
            DamageProc(abilityOwner, targetEnemy);
            nextProcTimer = Time.time + procSpacing;
        }

        if(abilityEndTimer <= Time.time) {
            ExitAbility();
        }

    } //end AbilityMap

}