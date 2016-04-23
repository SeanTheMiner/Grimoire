using UnityEngine;
using System.Collections;
using Procs;

public class Eviscerate : ChampionAbility {

    public DamageProc offensiveDamageProc = new DamageProc();
    public DamageProc defensiveDamageProc = new DamageProc();
    public EffectProc effectProc = new EffectProc();

    public Eviscerate() {

        abilityName = "Eviscerate";
        abilityType = AbilityType.Barrage;
        targetScope = TargetScope.Untargeted;
        primaryDamageType = DamageType.Physical;
        manaCost = 150;

        requiresTargeting = false;

        chargeDuration = 6;
        abilityDuration = 5;
        cooldownDuration = 30;

        offensiveDamageProc.procDamage = 100;
        offensiveDamageProc.damageType = DamageProc.DamageType.Physical;
        offensiveDamageProc.procSpacing = 0.2f;
        offensiveDamageProc.critChance = 40;
        offensiveDamageProc.critMultiplier = 3;

        defensiveDamageProc.procDamage = 120;
        defensiveDamageProc.damageType = DamageProc.DamageType.Physical;
        defensiveDamageProc.procSpacing = 0.4f;
        defensiveDamageProc.physicalFinesse = 80;
        
    } //end Constructor()

    public override void AbilityMap() {

        if (nextProcTimer <= Time.time) {

            targetEnemy = targetingManager.TargetRandomEnemy();
            
            if (ownerChampion.currentStance == Champion.ChampionStance.Offensive) {
                DetermineHitOutcomeSingle(abilityOwner, targetEnemy, offensiveDamageProc);
                nextProcTimer = Time.time + offensiveDamageProc.procSpacing;
            }
            else if (ownerChampion.currentStance == Champion.ChampionStance.Defensive) {
                DetermineHitOutcomeSingle(abilityOwner, targetEnemy, defensiveDamageProc);
                nextProcTimer = Time.time + defensiveDamageProc.procSpacing;
                abilityOwner.currentMana += 30;
            }
            
        } //end if time to proc

        if (abilityEndTimer <= Time.time) {
            ExitAbility();
        }

    } //end AbilityMap()



} //end Eviscerate class