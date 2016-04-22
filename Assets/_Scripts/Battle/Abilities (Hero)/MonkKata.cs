using UnityEngine;
using System.Collections;
using Abilities;
using Procs;

public class MonkKata : HeroAbility {

    public DamageProc firstProc = new DamageProc();
    public DamageProc secondProc = new DamageProc();
    public DamageProc thirdProc = new DamageProc();
    public DamageProc fourthProc = new DamageProc();
    public DamageProc fifthProc = new DamageProc();
    //public EffectProc effectProc = new EffectProc();
    //this is a stun eventually
    
    public float interProcSpacing;
    public float interProcTimer;
    public float chainContinueChance;
    public float chainDecayRate;
    public bool isChaining;
    

	public MonkKata () {

        abilityName = "Monk Kata";
        abilityType = AbilityType.InfBarrage;
        targetScope = TargetScope.Untargeted;
        primaryDamageType = DamageType.Physical;

        chargeDuration = 2;

        procSpacing = 0.6f;
        interProcSpacing = 2;
        interProcTimer = 0;
        chainContinueChance = 100;
        chainDecayRate = 15;

        isChaining = false;
        requiresTargeting = false;
        hasCooldown = false;

        firstProc.procDamage = 40;
        secondProc.procDamage = 55;
        thirdProc.procDamage = 75;
        fourthProc.procDamage = 100;
        fifthProc.procDamage = 130;

        firstProc.damageType = DamageProc.DamageType.Physical;
        secondProc.damageType = DamageProc.DamageType.Physical;
        thirdProc.damageType = DamageProc.DamageType.Physical;
        fourthProc.damageType = DamageProc.DamageType.Physical;
        fifthProc.damageType = DamageProc.DamageType.Physical;

        firstProc.critChance = 30;
        secondProc.critChance = 30;
        thirdProc.critChance = 40;
        fourthProc.critChance = 40;
        fifthProc.critChance = 50;

        firstProc.critMultiplier = 2;
        secondProc.critMultiplier = 2;
        thirdProc.critMultiplier = 2.5f;
        fourthProc.critMultiplier = 2.5f;
        fifthProc.critMultiplier = 3;

    } //end Constructor()


    public override void AbilityMap() {

        if (isChaining != true) {
            if (interProcTimer <= Time.time) {
                isChaining = true;
                targetEnemy = targetingManager.TargetRandomEnemy();
            }
            else {
                return;
            }
        } //end if isChaining = false

        if (nextProcTimer <= Time.time) {

            if (Random.Range(0, 100) <= chainContinueChance) {
                if (procCounter == 0) {
                    ProcessProc(firstProc);
                }
                else if (procCounter == 1) {
                    ProcessProc(secondProc);
                }
                else if (procCounter == 2) {
                    ProcessProc(thirdProc);
                }
                else if (procCounter == 3) {
                    ProcessProc(fourthProc);
                }
                else if (procCounter == 4) {
                    ProcessProc(fifthProc);
                    //EffectProc
                    ExitChain();
                }
            } //end if chain continued

            else {
                ExitChain();
            }
            
        } //end if next proc timer
        
    } //end AbilityMap()


    private void ProcessProc (DamageProc proc) {
        DetermineHitOutcomeSingle(abilityOwner, targetEnemy, proc);
        nextProcTimer = Time.time + procSpacing;
        procCounter++;
        chainContinueChance -= chainDecayRate;
    } //end ProcessProc(1)


    private void ExitChain () {
        isChaining = false;
        procCounter = 0;
        chainContinueChance = 100;
        procDamage = 100;
        interProcTimer = Time.time + interProcSpacing;
    } //end ExitChain()


} //end MonkKata class
