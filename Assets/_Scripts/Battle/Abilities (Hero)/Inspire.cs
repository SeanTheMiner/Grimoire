using UnityEngine;
using System.Collections;
using Procs;

public class Inspire : ChampionAbility {

    public HealProc healProc = new HealProc();
    public EffectProc effectProcOff = new EffectProc();
    public EffectProc effectProcDef = new EffectProc();

    public Inspire() {

        abilityName = "Inspire";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.SingleHero;
        abilityDamageType = AbilityDamageType.Healing;
        manaCost = 100;
        
        chargeDuration = 3.0f;
        cooldownDuration = 17;
       
        healProc.procHeal = 220;

        effectProcDef.effectApplied = new InspireEffect();
        effectProcOff.effectApplied = new InspireOffEffect();

    } //end Constructor()


    public override void AbilityMap() {

        CheckTarget();
        healProc.HealProcSingle(abilityOwner, targetHero);

        if (ownerChampion.currentStance == Champion.ChampionStance.Defensive) {
            effectProcDef.ApplyEffectSingle(effectProcDef.effectApplied, targetHero);
        }
        else if (ownerChampion.currentStance == Champion.ChampionStance.Offensive) {
            effectProcOff.ApplyEffectSingle(effectProcOff.effectApplied, targetHero);
        }
        
        ExitAbility();

    } //end AbilityMap()


} //end Ability