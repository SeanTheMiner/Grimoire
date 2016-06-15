using UnityEngine;
using System.Collections;

using Effects;
using BattleObjects;
using Procs;

public class AxeCleaveEff : Effect {

    public DamageProc bleedDamageProc = new DamageProc();
    
    public AxeCleaveEff() {

        effectName = "Eviscerated";
        effectIconText = "EV";
        effectDuration = 15;
        effectType = EffectType.Lump;
        statType = StatType.None;

        bleedDamageProc.procSpacing = 1;

    } //End Constructor()


    public override void EffectUpdate(BattleObject host) {

        Debug.Log(bleedDamageProc.nextProcTimer);


        if (bleedDamageProc.nextProcTimer <= Time.time) {

            Debug.Log("timer");

            bleedDamageProc.ApplyDamageProc(null, host);
            bleedDamageProc.nextProcTimer = Time.time + bleedDamageProc.procSpacing;
        }
        
    } //End EffectUpdate(1)



} //End AxeCleaveEff class