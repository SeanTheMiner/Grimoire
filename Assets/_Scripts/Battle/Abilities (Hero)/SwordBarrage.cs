using UnityEngine;
using System.Collections;
using BattleObjects;
using Procs;


public class SwordBarrage : ChampionAbility {
    
    public DamageProc primaryDamageProc = new DamageProc();
    public DamageProc secondaryDamageProc = new DamageProc();
    public HealProc healProc = new HealProc();

    public SwordBarrage () {

        abilityName = "Sword Barrage";
        abilityType = AbilityType.Barrage;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;
        manaCost = 80;

        chargeDuration = 3.0f;
        abilityDuration = 4;
        cooldownDuration = 12.0f;
        procSpacing = 0.6f;
        
        primaryDamageProc.procDamage = 60;
        primaryDamageProc.damageType = DamageProc.DamageType.Physical;
        primaryDamageProc.hasDependent = true;
        primaryDamageProc.dependentProc = secondaryDamageProc;

        secondaryDamageProc.procDamage = 30;
        secondaryDamageProc.damageType = DamageProc.DamageType.Magical;
        secondaryDamageProc.isDependent = true;
        secondaryDamageProc.dependentUponProc = primaryDamageProc;

        healProc.procHeal = 25;
        
    } //end constructor


    public override void AbilityMap() {

        if (nextProcTimer <= Time.time) {

            CheckTarget();
            DetermineHitOutcomeSingle(abilityOwner, targetEnemy, primaryDamageProc);

            if (ownerChampion.currentStance == Champion.ChampionStance.Defensive) {
                healProc.HealProcSingle(abilityOwner, abilityOwner);
            }
            else if (ownerChampion.currentStance == Champion.ChampionStance.Offensive) {
                secondaryDamageProc.ApplyDamageProc(abilityOwner, targetEnemy);
            }

            nextProcTimer = Time.time + procSpacing;

        } //end if time to proc
        
        if (abilityEndTimer <= Time.time) {
            ExitAbility();
        }
        
    } //end AbilityMap()

    
} //end SwordBarrage class
