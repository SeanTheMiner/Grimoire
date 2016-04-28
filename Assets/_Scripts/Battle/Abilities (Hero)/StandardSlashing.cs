using UnityEngine;
using System.Collections;
using Procs;

public class StandardSlashing : ChampionAbility {

    public DamageProc offensiveProc = new DamageProc();
    public DamageProc defensiveProc = new DamageProc();

    public StandardSlashing () {

        abilityName = "Standard Slashing";
        abilityType = AbilityType.InfBarrage;
        targetScope = TargetScope.SingleEnemy;
        abilityDamageType = AbilityDamageType.Physical;

        costsMana = false;
        hasCooldown = false;

        chargeDuration = 2;

        offensiveProc.procDamage = 50;
        offensiveProc.procSpacing = 0.7f;
        offensiveProc.damageType = DamageProc.DamageType.Physical;
        offensiveProc.physicalPenetration = 80;

        defensiveProc.procDamage = 50;
        defensiveProc.procSpacing = 1;
        offensiveProc.damageType = DamageProc.DamageType.Physical;
        offensiveProc.physicalFinesse = 50;
        offensiveProc.physicalAccuracy = 50;

    } //end Constructor()


    public override void AbilityMap() {
        
        if (ownerChampion.currentStance == Champion.ChampionStance.Offensive) {
            UpdateInfBarrageMask(nextProcTimer, offensiveProc.procSpacing);
        }
        else {
            UpdateInfBarrageMask(nextProcTimer, defensiveProc.procSpacing);
        }
        
        if (nextProcTimer <= Time.time) {
            
            CheckTarget();
            if (ownerChampion.currentStance == Champion.ChampionStance.Offensive) {
                offensiveProc.ApplyDamageProc(abilityOwner, targetEnemy);
                nextProcTimer = ApplySpacing(offensiveProc.procSpacing);
            }
            else if (ownerChampion.currentStance == Champion.ChampionStance.Defensive) {
                defensiveProc.ApplyDamageProc(abilityOwner, targetEnemy);
                nextProcTimer = ApplySpacing(defensiveProc.procSpacing);
            }
            
        } //end if time to proc

    } //end AbilityMap()
    

} //end StandardSlashing class
