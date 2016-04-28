using UnityEngine;
using System.Collections;
using Procs;

public class RaiseSpirits : HeroAbility {

    public HealProc healProc = new HealProc();
    public EffectProc effectProc = new EffectProc();
    
    public RaiseSpirits() {
        
        abilityName = "Raise Spirits";

        abilityType = AbilityType.Burst;
        targetScope = TargetScope.AllHeroes;
        abilityDamageType = AbilityDamageType.Healing;

        requiresTargeting = false;

        manaCost = 250;

        chargeDuration = 4.0f;
        cooldownDuration = 25;

        healProc.procHeal = 120;
        effectProc.effectApplied = new RaiseSpiritsEff();
        
    } //end Constructor()
    

    public override void AbilityMap() {

        targetBattleObjectList = targetingManager.TargetAllHeroes();
        effectProc.ApplyEffectMultiple(effectProc.effectApplied, targetBattleObjectList);
        healProc.HealProcMultiple(abilityOwner, targetBattleObjectList);
        ExitAbility();

    } //end AbilityMap()


} //end Ability