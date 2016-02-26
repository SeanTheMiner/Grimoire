using UnityEngine;
using System.Collections;
using Abilities;

public class HeroOneAbilityOne : Ability {

    public HeroOneAbilityOne() {
        
        abilityName = "Charge Punch";
        chargeDuration = 3.0f;
        cooldownDuration = 4.0f;
        procDamage = 150.0f;
        procLimit = 1;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;

    } //end constructor

    public override void InitAbility() {
        base.InitAbility();
        abilityOwner.currentBattleState = Heroes.Hero.BattleState.Burst;
    }


    public override void AbilityMap() {

        Debug.Log(targetEnemy.name);

        DamageProc(abilityOwner, targetEnemy);
        ExitAbility();

    } //end AbilityMap

} //end class
