using UnityEngine;
using System.Collections;

using Effects;
using Procs;
using ProcTriggers;
using BattleObjects;

public class EvadeCounterEff : Effect {
    
    public EvadeTrigger evadeTrigger = new EvadeTrigger();
    public DamageProc damageProc = new DamageProc();

    public EvadeCounterEff () {

	    effectName = "Evade and Counter";
        effectType = EffectType.Persistent;
        
        evadeTrigger.damageTypeConstraint = DamageProc.DamageType.Physical;
        evadeTrigger.triggerObject = ProcTrigger.TriggerObject.Self;
        evadeTrigger.evadeTriggerType = EvadeTrigger.EvadeTriggerType.Evaded;
        evadeTrigger.procTriggered = damageProc;
        evadeTrigger.procScope = ProcTrigger.ProcScope.OtherParty;

        damageProc.procDamage = 100;
        damageProc.damageType = DamageProc.DamageType.Magical;
        
    } //end Constructor()

    public override void InitEffect(BattleObject host) {
        evadeTrigger.triggerOwner = host;
        base.InitEffect(host);
    }







} //end EvadeCounterEff class

