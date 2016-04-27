using UnityEngine;
using System.Collections;
using Abilities;
using Procs;

public class EbbAndFlow : HeroAbility {

    public DamageProc damageProc = new DamageProc();
    public HealProc healProc = new HealProc();

	public EbbAndFlow() {

        abilityName = "Ebb and Flow";
        abilityType = AbilityType.InfBarrage;
        targetScope = TargetScope.Untargeted;
        abilityDamageType = AbilityDamageType.Magical;
        costsMana = false;

        requiresTargeting = false;
        hasCooldown = false;

        chargeDuration = 3;
        procSpacing = 0.8f;
        
        damageProc.procDamage = 90;
        damageProc.damageType = DamageProc.DamageType.Magical;
        damageProc.procSpacing = 1.5f;
        
        healProc.procHeal = 75;
        healProc.procSpacing = 1.5f;
        healProc.procStartDelay = 0.75f;
        
    } //end Constructor()


    public override void CheckCharge() {

        if (chargeEndTimer <= Time.time) {
            abilityOwner.currentBattleState = Heroes.Hero.BattleState.InfBarrage;
            abilityOwner.canTakeCommands = true;
            ApplySpacing(healProc.nextProcTimer, healProc.procStartDelay);
            AbilityMap();
        } //end if chargeEndTimer <= Time.time

    } //end CheckCharge() override


    public override void AbilityMap() {

        if (damageProc.nextProcTimer < healProc.nextProcTimer) {
            UpdateInfBarrageMask(damageProc.nextProcTimer, (damageProc.procSpacing / 2));
        }
        else {
            UpdateInfBarrageMask(healProc.nextProcTimer, (healProc.procSpacing / 2));
        }

        if (damageProc.nextProcTimer <= Time.time) {
            DetermineHitOutcomeSingle(abilityOwner, targetingManager.TargetRandomEnemy(), damageProc);
            ApplySpacing(damageProc.nextProcTimer, damageProc.procSpacing);
        } //end if damageProc time
        else if (healProc.nextProcTimer <= Time.time) {
            healProc.HealProcSingle(abilityOwner, targetingManager.TargetRandomHero());
            ApplySpacing(healProc.nextProcTimer, healProc.procSpacing);
        } //end if healProc time
        
    } //end AbilityMap()
    

} //end EbbAndFlow class
