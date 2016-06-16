using UnityEngine;
using System.Collections;

using Effects;
using BattleObjects;
using Procs;

public class AxeCleaveEff : Effect {

    public DamageProc damageProc = new DamageProc();
    
    public AxeCleaveEff() {

        effectName = "Eviscerated";
        effectIconText = "EV";
        effectDuration = 15;
        effectType = EffectType.Lump;
        statType = StatType.None;
        hasProcs = true;

        procSpacing = 1;

        damageProc.procDamage = 30;
        damageProc.damageType = DamageProc.DamageType.Magical;

    } //End Constructor()

    public override void ProcMap(BattleObject host) {

       damageProc.ApplyActorlessDamageProc(host);

    } //End ProcMap(1)
    

} //End AxeCleaveEff class