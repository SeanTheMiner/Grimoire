using UnityEngine;
using System.Collections;
using Procs;

public class BlessWeapon : HeroAbility {

    public EffectProc effectProc = new EffectProc();

    public BlessWeapon() {

        abilityName = "Bless Weapon";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.SingleHero;
        primaryDamageType = DamageType.None;
        manaCost = 60;

        chargeDuration = 4;
        cooldownDuration = 20;

        effectProc.effectApplied = new BlessWeaponEff();
        
    } //end Constructor()


    public override void AbilityMap() {

        CheckTarget();
        effectProc.ApplyEffectSingle(effectProc.effectApplied, targetHero);
        ExitAbility();
        
    } //end AbilityMap()

} //end BlessWeapon class
