using UnityEngine;
using System.Collections;
using Abilities;
using Procs;

public class FireStorm : HeroAbility {

    public DamageProc damageProc = new DamageProc();

    public FireStorm() {

        abilityName = "Fire Storm";

        abilityType = AbilityType.InfBarrage;
        targetScope = TargetScope.Untargeted;
        primaryDamageType = DamageType.Magical;

        costsMana = false;
        requiresTargeting = false;
        hasCooldown = false;

        chargeDuration = 2;
        
        damageProc.procDamage = 30;
        damageProc.damageType = DamageProc.DamageType.Magical;
        damageProc.procSpacing = 0.4f;
        
    } //end Constructor()


    public override void AbilityMap() {

        if (damageProc.nextProcTimer <= Time.time) {
            targetingManager.TargetRandomEnemy(this);
            DetermineHitOutcomeSingle(abilityOwner, targetEnemy, damageProc);
            damageProc.nextProcTimer = Time.time + damageProc.procSpacing;
        }
       
    } //end Ability Map()
    
} //end FireStorm class