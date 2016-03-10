using UnityEngine;
using System.Collections;
using Abilities;
using Enemies;

public class PiercingFire : Ability
{

    public PiercingFire()
    {

        abilityName = "Piercing Fire";
        requiresTargeting = false;
        chargeDuration = 3.0f;
        cooldownDuration = 5.0f;
        procDamage = 80.0f;
        targetScope = TargetScope.AllEnemies;
        primaryDamageType = DamageType.Magical;

    }

    public override void AbilityMap()
    {
        ApplyEffectMultiple(effectApplied);
        DamageProcMultiple(abilityOwner);
        ExitAbility();

    } //end AbilityMap()

    public override void SetBattleState()
    {
        abilityOwner.currentBattleState = Heroes.Hero.BattleState.Burst;
    }

    public override void ClearTargeting()
    {
        targetBattleObjectList.Clear();
    }

} //end Ability