using UnityEngine;
using System.Collections;
using Procs;

public class PiercingFire : HeroAbility {

    public DamageProc centerDamageProc = new DamageProc();
    public DamageProc AOEDamageProc = new DamageProc();
    

    public PiercingFire() {

        abilityName = "Piercing Fire";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.SingleEnemy;
        abilityDamageType = AbilityDamageType.Magical;
        
        manaCost = 100;
        chargeDuration = 1;
        cooldownDuration = 18;

        centerDamageProc.procDamage = 120;
        centerDamageProc.damageType = DamageProc.DamageType.Physical;

        AOEDamageProc.procDamage = 120;
        AOEDamageProc.damageType = DamageProc.DamageType.Magical;

        radiusOfAOE = 6;

    } //end Constructor()

    
    public override void AbilityMap() {

        CheckTarget();
        DetermineHitOutcomeSingle(abilityOwner, targetEnemy, centerDamageProc);
        DetermineHitOutcomeMultiple(abilityOwner, CheckAOETargetsOnCenter(targetEnemy), AOEDamageProc);
        ExitAbility();

    } //end AbilityMap()


} //end Ability