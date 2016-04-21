using UnityEngine;
using System.Collections.Generic;

using Effects;
using BattleObjects;
using Procs;
using ProcTriggers;

public class EventManager : MonoBehaviour {

    public List<EvadeTrigger> evadeTriggerList = new List<EvadeTrigger>();
    public TargetingManager targetingManager;


    public void CheckEvaderForTriggers(BattleObject evader, BattleObject misser, DamageProc proc) {
        foreach (ProcTrigger trigger in evader.procTriggerList) {
            if ((trigger is EvadeTrigger) && ((trigger as EvadeTrigger).evadeTriggerType == EvadeTrigger.EvadeTriggerType.Evaded)) {
                if (trigger.procTriggered is DamageProc) {
                    if (trigger.procScope == ProcTrigger.ProcScope.OtherParty
                    (trigger.procTriggered as DamageProc).ApplyDamageProc(evader, misser);
                }
                else if (trigger.procTriggered is HealProc) {
                    (trigger.procTriggered as HealProc).HealProcSingle(evader, evader);
                }
                else if (trigger.procTriggered is )
            }
        }
    } //end CheckEvaderForTriggers



    public void ActivateDamageTrigger(BattleObject host, BattleObject otherParty, ProcTrigger trigger) {
        if (trigger.procScope == ProcTrigger.ProcScope.OtherParty) {
            (trigger.procTriggered as DamageProc).ApplyDamageProc(host, otherParty);
        }
        else if (trigger.procScope == ProcTrigger.ProcScope.AllEnemies) {
            targetingManager.TargetAllHeroes();
            //YOu need to change targetingManager to return a list of battleobjects, and then
            //calls on abilities to pass that to the damageprocmultiple? or just set their list to that. Either way.
        }

    }


    public void CheckForEvadeTrigger(BattleObject attacker, BattleObject evader, DamageProc proc) {
      
        foreach (EvadeTrigger evadeTrigger in evadeTriggerList) {
            if ((evadeTrigger.triggerOwner == attacker) | (evadeTrigger.triggerOwner == evader)) {
                evadeTrigger.CheckTrigger(attacker, evader, proc);
            }
        }
    } //end CheckForEvadeTrigger

} //end EventManager class
