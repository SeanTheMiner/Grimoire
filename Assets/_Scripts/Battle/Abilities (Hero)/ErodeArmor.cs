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
        primaryDamageType = DamageType.Magical;
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

        targetBattleObjectList = targetingManager.TargetAllEnemies();
        DetermineHitOutcomeMultiple(abilityOwner, damageProc);
        effectProc.ApplyEffectMultiple(effectProc.effectApplied, targetBattleObjectList);
        ExitAbility();

    } //end AbilityMap()


} //end Ability