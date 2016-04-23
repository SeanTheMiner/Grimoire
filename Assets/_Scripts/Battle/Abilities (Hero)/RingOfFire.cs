using UnityEngine;
using Procs;
using Effects;

public class RingOfFire : HeroAbility {

    public DamageProc damageProc = new DamageProc();
    public Effect effectToCheckFor;

    public RingOfFire() {

        abilityName = "Ring of Fire";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.FreeTargetAOE;
        primaryDamageType = DamageType.Magical;
        manaCost = 120;

        canBeDefault = false;
        appliesCoreEffect = true;

        chargeDuration = 4.0f;
        cooldownDuration = 12.0f;
        radiusOfAOE = 4;

        damageProc.procDamage = 200;
        damageProc.damageType = DamageProc.DamageType.Magical;
        
    } //end Constructor()


    public override void SetCoreEffectApplied() {
        effectToCheckFor = coreEffectApplied;
    }


    public override void AbilityMap() {
        
        EffectController effectController = FindEffectController(effectToCheckFor);

        if (effectController != null) {
            int modifierStacks = effectController.CountAllStacks();
            damageProc.procDamage += (0.1f * modifierStacks);
        }

        DetermineHitOutcomeMultiple(abilityOwner, CheckAOETargets(), damageProc);
        damageProc.procDamage = 200;

        if (effectController != null) {
            foreach (EffectController.HostController hostController in effectController.hostControllerList) {
                effectController.UpdateStacks(hostController, Mathf.RoundToInt(hostController.stackCount / -2));
            }
        }

        ExitAbility();

    } //end AbilityMap()


} //end RingOfFire class
