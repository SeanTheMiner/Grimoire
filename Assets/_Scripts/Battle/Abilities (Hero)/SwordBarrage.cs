using UnityEngine;
using System.Collections;
using BattleObjects;


public class SwordBarrage : HeroAbility {

    public Champion ownerChampion;

    public float secondaryProcDamage {
        get; set;
    }

    public SwordBarrage () {

        abilityName = "Sword Barrage";
        abilityType = AbilityType.Barrage;
        targetScope = TargetScope.SingleEnemy;
        primaryDamageType = DamageType.Physical;
        manaCost = 80;

        chargeDuration = 3.0f;
        abilityDuration = 4;
        cooldownDuration = 12.0f;

        procDamage = 60.0f;
        procHeal = 25;
        secondaryProcDamage = 35;

        procSpacing = 0.5f;
        critChance = 30;
        critMultiplier = 2.5f;

    } //end constructor


    public override void AbilityMap() {

        if (nextProcTimer <= Time.time) {
            CheckTarget();
            DetermineHitOutcomeSingle(abilityOwner, targetEnemy);
            ApplyStanceEffect();
            nextProcTimer = Time.time + procSpacing;
        }

        if (abilityEndTimer <= Time.time) {
            ExitAbility();
        }
        
    } //end AbilityMap()


    private void ApplyStanceEffect() {

        if (ownerChampion.currentStance == Champion.ChampionStance.Defensive) {
            HealProcSingle(ownerChampion, ownerChampion);
        }
        else {
            SecondaryDamageProc(targetEnemy, secondaryProcDamage);
        }


    }


    public void SecondaryDamageProc (BattleObject defender, float damage) {

        int damageToApply = Mathf.RoundToInt(HitManager.ApplyResist(abilityOwner, defender, this, damage));
        defender.currentHealth -= damageToApply;
        defender.SpawnDamageText(damageToApply, Abilities.Ability.DamageType.Magical);
        
    }


} //end SwordBarrage class
