using UnityEngine;
using System.Collections;
using Abilities;
using Enemies;
using Procs;

public class EndlessFire : HeroAbility {

    public DamageProc damageProc = new DamageProc();

    public EndlessFire() {

        abilityName = "Endless Fire";

        abilityType = AbilityType.InfCharge;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Magical;

        requiresTargeting = true;

        manaCost = 10;
        cooldownDuration = 5.0f;
        infProcMultiplier = 70.0f;
        damageProc.damageType = DamageProc.DamageType.Magical;

    } //end Constructor()


    public override void AbilityMap() {

        targetingManager.SortTargetingType(this);
        InfDetermineHitOutcomeSingle(abilityOwner, targetEnemy, damageProc, this);
        ExitAbility();

    } //end AbilityMap()


} //end EndlessFire() (HAHAHA)
