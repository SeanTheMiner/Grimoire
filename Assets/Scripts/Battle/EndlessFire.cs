using UnityEngine;
using System.Collections;
using Abilities;
using Enemies;

public class EndlessFire : Ability {

    public EndlessFire() {

        abilityName = "Endless Fire";
        requiresTargeting = true;
        isInfCharge = true;
        cooldownDuration = 5.0f;
        infProcMultiplier = 70.0f;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Magical;

    }


    public override void AbilityMap() {
       
        InfDamageProc(abilityOwner, targetEnemy, infProcMultiplier);
        ExitAbility();

    } //end AbilityMap()

    public override void SetBattleState() {
        abilityOwner.currentBattleState = Heroes.Hero.BattleState.InfCharge;
    }

    public override void ClearTargeting() {
        targetEnemy = null;
    }

} //end EndlessFire() (HAHAHA)
