using UnityEngine;
using System.Collections;

using Effects;
using Procs;
using ProcTriggers;
using BattleObjects;

public class BlockAndHealEff : Effect {

    public ProcTrigger procTrigger = new ProcTrigger();
    public HealProc healProc = new HealProc();

    public BlockAndHealEff() {

        effectName = "Block and Heal";
        effectType = EffectType.Persistent;

        procTrigger.procTriggerName = "Block and Heal";

        procTrigger.triggerType = ProcTrigger.TriggerType.Block;
        procTrigger.objectRole = ProcTrigger.ObjectRole.Receiver;
        procTrigger.approvedDamageTypeList.Add(DamageProc.DamageType.Magical);

        procTrigger.procTriggered = healProc;
        procTrigger.procScope = ProcTrigger.ProcScope.AllHeroes;

        healProc.procHeal = 40;

    } //end Constructor()


    public override void InitEffect(BattleObject host) {
        InitTrigger(host, procTrigger);
        base.InitEffect(host);
    }
    
} //end BlockAndHealEff class
