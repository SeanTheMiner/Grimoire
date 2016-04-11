using UnityEngine;
using System.Collections;
using Abilities;

public class MonkKata : HeroAbility {

    
    public float interProcSpacing;
    public float interProcTimer;
    public float chainContinueChance;
    public bool isChaining;

	public MonkKata () {

        abilityName = "Monk Kata";
        abilityType = AbilityType.InfBarrage;
        targetScope = TargetScope.Untargeted;
        primaryDamageType = DamageType.Physical;

        chargeDuration = 2;
        procDamage = 30;
        procSpacing = 0.35f;

        interProcSpacing = 2.5f;
        interProcTimer = 0;
        chainContinueChance = 100;
        isChaining = false;

        requiresTargeting = false;
        hasCooldown = false;
        

    }

    public override void AbilityMap() {

        if (isChaining != true) {
            if (interProcTimer <= Time.time) {
                isChaining = true;
                targetingManager.TargetRandomEnemy(this);
            }
            else {
                return;
            }
        } //end if isChaining = false

        if (nextProcTimer <= Time.time) {
            procDamage += 30;
            if (Random.Range(0, 100) <= chainContinueChance) {
                DetermineHitOutcomeSingle(abilityOwner, targetEnemy);
                nextProcTimer = Time.time + procSpacing;
                procCounter++;
                chainContinueChance *= 0.75f;
            }
            else {
                isChaining = false;
                procCounter = 0;
                chainContinueChance = 100;
                procDamage = 100;
                interProcTimer = Time.time + interProcSpacing;
            }
            
        } //end if next proc timer

    } //end AbilityMap()


} //end MonkKata class
