using UnityEngine;
using System.Collections;
using Effects;
using BattleObjects;
using ProcTriggers;
using Procs;

public class BlessWeaponEff : Effect {

    public ProcTrigger procTrigger = new ProcTrigger();
    public DamageProc damageProc = new DamageProc();

    public BlessWeaponEff() {

        effectName = "Bless Weapon";
        effectIconText = "BW";
        effectDuration = 10;
        statType = StatType.None;
        effectType = EffectType.Lump;

        procTrigger.procTriggerName = "Blessed Weapon";

        procTrigger.triggerType = ProcTrigger.TriggerType.Damage;
        procTrigger.objectRole = ProcTrigger.ObjectRole.Actor;
        procTrigger.approvedDamageTypeList.Add(DamageProc.DamageType.Physical);

        procTrigger.procTriggered = damageProc;
        procTrigger.procScope = ProcTrigger.ProcScope.OtherParty;

        damageProc.procDamage = 40;
        damageProc.damageType = DamageProc.DamageType.Magical;

    } //end Constructor()


    public override void InitEffect(BattleObject host) {
        InitTrigger(host, procTrigger);
        base.InitEffect(host);
    }

    public override void RemoveEffect(BattleObject host) {
        RemoveTrigger(host, procTrigger);
        base.RemoveEffect(host);
    }
    

} //end BlessWeaponEff class
