using UnityEngine;
using Procs;

public class Maelstrom : HeroAbility {

    public DamageProc magicalDamageProc = new DamageProc();
    public DamageProc physicalDamageProc = new DamageProc();
    
    public Maelstrom() {

        abilityName = "Maelstrom";
        abilityType = AbilityType.Barrage;
        targetScope = TargetScope.FreeTargetAOE;
        primaryDamageType = DamageType.Magical;
        manaCost = 200;

        canBeDefault = false;

        chargeDuration = 6.0f;
        abilityDuration = 4.0f;
        cooldownDuration = 20.0f;
        radiusOfAOE = 5;

        magicalDamageProc.procDamage = 45;
        magicalDamageProc.damageType = DamageProc.DamageType.Magical;
        magicalDamageProc.procSpacing = 0.6f;

        physicalDamageProc.procDamage = 80;

    } //end Constructor()


    public override void AbilityMap() {

        if (magicalDamageProc.nextProcTimer <= Time.time) {
            DetermineHitOutcomeMultiple(abilityOwner, CheckAOETargets(), magicalDamageProc);
            ClearTargeting();
            magicalDamageProc.nextProcTimer = Time.time + magicalDamageProc.procSpacing;
        }

        if (abilityEndTimer <= Time.time) {
            physicalDamageProc.procDamage *= targetBattleObjectList.Count;
            DetermineHitOutcomeMultiple(abilityOwner, CheckAOETargets(), physicalDamageProc);
            ExitAbility();
        }
        
    } //end AbilityMap()

   
} //end RingOfFire class
