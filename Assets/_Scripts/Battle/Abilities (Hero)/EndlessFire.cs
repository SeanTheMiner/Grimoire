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
        abilityDamageType = AbilityDamageType.Magical;

        requiresTargeting = true;

        manaCost = 10;
        cooldownDuration = 5.0f;
        infProcMultiplier = 70.0f;
        damageProc.damageType = DamageProc.DamageType.Magical;

    } //end Constructor()


    public override void AbilityMap() {

        targetingManager.SortTargetingType(this);
        InfDetermineHitOutcomeSingle(abilityOwner, targetEnemy, this, damageProc);
        ExitAbility();

    } //end AbilityMap()


} //end EndlessFire() (HAHAHA)
