using UnityEngine;
using System.Collections;
using Abilities;
using Procs;


public class ErodeArmor : HeroAbility {

    public DamageProc damageProc = new DamageProc();
    public EffectProc effectProc = new EffectProc();

    public ErodeArmor() {

        abilityName = "Erode Armor";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.AllEnemies;
        abilityDamageType = AbilityDamageType.Magical;
        manaCost = 120;

        requiresTargeting = false;

        chargeDuration = 6;
        cooldownDuration = 20;

        damageProc.damageType = DamageProc.DamageType.Magical;
        damageProc.procDamage = 100.0f;
        damageProc.magicalFinesse = 80;

        effectProc.effectApplied = new ErodeArmorEff();

    } //end Constructor()

    public override void AbilityMap() {

        DetermineHitOutcomeMultiple(abilityOwner, targetingManager.TargetAllEnemies(), damageProc);
        effectProc.ApplyEffectMultiple(effectProc.effectApplied, targetingManager.TargetAllEnemies());
        ExitAbility();

    } //end AbilityMap()


} //end Ability