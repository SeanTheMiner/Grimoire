using UnityEngine;
using System.Collections;
using Abilities;
using Enemies;

public class EndlessFire : HeroAbility {

    public EndlessFire() {

        abilityName = "Endless Fire";
        abilityType = AbilityType.InfCharge;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Magical;
        manaCost = 150;

        requiresTargeting = true;
        
        cooldownDuration = 5.0f;
        infProcMultiplier = 70.0f;
        
    }


    public override void AbilityMap() {

        targetingManager.SortTargetingType(this);
        InfDamageProc(abilityOwner, targetEnemy, infProcMultiplier);
        ExitAbility();

    } //end AbilityMap()


} //end EndlessFire() (HAHAHA)
