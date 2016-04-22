using UnityEngine;
using System.Collections;

using Effects;
using Procs;
using ProcTriggers;
using BattleObjects;

public class EvadeCounterEff : Effect {
    
    public ProcTrigger procTrigger = new ProcTrigger();
    public DamageProc damageProc = new DamageProc();

    public EvadeCounterEff () {

	    effectName = "Evade and Counter";
        effectType = EffectType.Persistent;

        procTrigger.procTriggerName = "Evade and Counter";

        procTrigger.triggerType = ProcTrigger.TriggerType.Evasion;
        procTrigger.objectRole = ProcTrigger.ObjectRole.Receiver;
        procTrigger.approvedDamageTypeList.Add(DamageProc.DamageType.Physical);

        procTrigger.procTriggered = damageProc;
        procTrigger.procScope = ProcTrigger.ProcScope.OtherParty;

        damageProc.procDamage = 100;
        damageProc.damageType = DamageProc.DamageType.Magical;
        
    } //end Constructor()


    public override void InitEffect(BattleObject host) {
        InitTrigger(host, procTrigger);
        base.InitEffect(host);
    }
    
} //end EvadeCounterEff class

