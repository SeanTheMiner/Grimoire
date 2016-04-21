using UnityEngine;
using System.Collections.Generic;

using Effects;
using BattleObjects;
using Procs;
using ProcTriggers;

public class EventManager : MonoBehaviour {

    public List<EvadeTrigger> evadeTriggerList = new List<EvadeTrigger>();

    public void CheckForEvadeTrigger(BattleObject attacker, BattleObject evader, DamageProc proc) {
      
        foreach (EvadeTrigger evadeTrigger in evadeTriggerList) {
            if ((evadeTrigger.triggerOwner == attacker) | (evadeTrigger.triggerOwner == evader)) {
                evadeTrigger.CheckTrigger(attacker, evader, proc);
            }
        }
    } //end CheckForEvadeTrigger

} //end EventManager class
