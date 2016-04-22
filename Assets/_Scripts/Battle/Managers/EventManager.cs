using UnityEngine;
using System.Collections.Generic;

using Effects;
using BattleObjects;
using Heroes;
using Enemies;
using Procs;
using ProcTriggers;

public class EventManager : MonoBehaviour {

    public TargetingManager targetingManager;
    
    public void CheckForTriggers (BattleObject actor, BattleObject receiver, ProcTrigger.TriggerType triggerType, DamageProc.DamageType damageType) {

        if (actor.procTriggerList.Count > 0) {
            foreach (ProcTrigger trigger in actor.procTriggerList) {
                if ((trigger.triggerType == triggerType)
                    && (trigger.objectRole == ProcTrigger.ObjectRole.Actor)
                    && (trigger.approvedDamageTypeList.Contains(damageType))
                    ) {
                    SortActivationType(actor, receiver, trigger);
                } //end if(3) for actor
            } //end actor foreach
        } //end actor list count check

        if (receiver.procTriggerList.Count > 0) {
            foreach (ProcTrigger trigger in receiver.procTriggerList) {
                if ((trigger.triggerType == triggerType)
                    && (trigger.objectRole == ProcTrigger.ObjectRole.Receiver)
                    && (trigger.approvedDamageTypeList.Contains(damageType))
                    ) {
                    SortActivationType(receiver, actor, trigger);
                } //end if(3) for receiver
            } //end receiver foreach
        } //end receiver list count check

    } //end CheckActorForTriggers(4)

    
    public void SortActivationType (BattleObject host, BattleObject otherParty, ProcTrigger trigger) {

        if (trigger.procTriggered is DamageProc) {
            ActivateDamageTrigger(host, otherParty, trigger);
        }
        else if (trigger.procTriggered is HealProc) {
            ActivateHealTrigger(host, otherParty, trigger);
        }
        else if (trigger.procTriggered is EffectProc) {
            ActivateEffectTrigger(host, otherParty, trigger);
        }

    } //end SortActivationType(3)
    

    public void ActivateDamageTrigger(BattleObject host, BattleObject otherParty, ProcTrigger trigger) {

        if (trigger.procScope == ProcTrigger.ProcScope.OtherParty) {
            (trigger.procTriggered as DamageProc).ApplyDamageProc(host, otherParty);
            return;
        }
        else if (trigger.procScope == ProcTrigger.ProcScope.AllEnemies) {
            (trigger.procTriggered as DamageProc).ApplyDamageProcMultiple(host, targetingManager.TargetAllEnemies());
            return;
        }

        //Other procScopes can go here, but these are the two that make the most sense, 
        //and other ones would probably need more information on the trigger itself (like a linked battleobject to proc toward)

    } //end ActivateDamageTrigger(3)


    public void ActivateHealTrigger (BattleObject host, BattleObject otherParty, ProcTrigger trigger) {

        if (trigger.procScope == ProcTrigger.ProcScope.Self) {
            (trigger.procTriggered as HealProc).HealProcSingle(host, host);
            return;
        }
        else if (trigger.procScope == ProcTrigger.ProcScope.AllHeroes) {
            (trigger.procTriggered as HealProc).HealProcMultiple(host, targetingManager.TargetAllHeroes());
            return;
        }

    } //end ActivateHealTrigger(3)

   
    public void ActivateEffectTrigger(BattleObject host, BattleObject otherParty, ProcTrigger trigger) {

        if (trigger.procScope == ProcTrigger.ProcScope.Self) {
            (trigger.procTriggered as EffectProc).ApplyEffectSingle((trigger.procTriggered as EffectProc).effectApplied, host);
            return;
        }
        else if (trigger.procScope == ProcTrigger.ProcScope.OtherParty) {
            (trigger.procTriggered as EffectProc).ApplyEffectSingle((trigger.procTriggered as EffectProc).effectApplied, otherParty);
            return;
        }
        else if (trigger.procScope == ProcTrigger.ProcScope.AllEnemies) {
            (trigger.procTriggered as EffectProc).ApplyEffectMultiple((trigger.procTriggered as EffectProc).effectApplied, targetingManager.TargetAllEnemies());
            return;
        }
        else if (trigger.procScope == ProcTrigger.ProcScope.AllHeroes) {
            (trigger.procTriggered as EffectProc).ApplyEffectMultiple((trigger.procTriggered as EffectProc).effectApplied, targetingManager.TargetAllHeroes());
        }

    } //end ActivateEffectTrigger(3)
    

} //end EventManager class






//Collective functions
/*
public void CheckForDamageTriggers(BattleObject attacker, BattleObject defender, Proc proc) {
    CheckAttackerForTriggers(attacker, defender, proc);
    CheckDefenderForTriggers(attacker, defender, proc);
}

public void CheckForCritTriggers(BattleObject critter, BattleObject critee, Proc proc) {

}

public void CheckForEvadeTriggers(BattleObject evader, BattleObject misser, Proc proc) {
    CheckEvaderForTriggers(evader, misser, proc);
    CheckMisserForTriggers(evader, misser, proc);
}

public void CheckForBlockTriggers(BattleObject blocker, BattleObject blockee, Proc proc) {
    CheckBlockerForTriggers(blocker, blockee, proc);
    CheckBlockeeForTriggers(blocker, blockee, proc);
}


public void CheckAttackerForTriggers(BattleObject attacker, BattleObject defender, Proc proc) {

   foreach (ProcTrigger trigger in attacker.procTriggerList) {
       if ((trigger is DamageTrigger) && ((trigger as DamageTrigger).damageTriggerType == DamageTrigger.DamageTriggerType.Damaged)) {
           if (trigger.procTriggered is DamageProc) {
               ActivateDamageTrigger(attacker, defender, trigger);
           }
           else if (trigger.procTriggered is HealProc) {
               ActivateHealTrigger(attacker, defender, trigger);
           }
           else if (trigger.procTriggered is EffectProc) {
               ActivateEffectTrigger(attacker, defender, trigger);
           }
       } //end if DamageTrigger 
   } //end foreach

} //end CheckAttackerForTriggers


public void CheckDefenderForTriggers(BattleObject attacker, BattleObject defender, Proc proc) {

   foreach (ProcTrigger trigger in attacker.procTriggerList) {
       if ((trigger is DamageTrigger) && ((trigger as DamageTrigger).damageTriggerType == DamageTrigger.DamageTriggerType.WasDamaged)) {
           if (trigger.procTriggered is DamageProc) {
               ActivateDamageTrigger(defender, attacker, trigger);
           }
           else if (trigger.procTriggered is HealProc) {
               ActivateHealTrigger(defender, attacker, trigger);
           }
           else if (trigger.procTriggered is EffectProc) {
               ActivateEffectTrigger(defender, attacker, trigger);
           }
       } //end if DamageTrigger 
   } //end foreach

} //end CheckDefenderForTriggers



//CritTrigger functions

public void CheckCritterForTriggers(BattleObject attacker, BattleObject defender, Proc proc) {

   foreach (ProcTrigger trigger in attacker.procTriggerList) {
       if ((trigger is DamageTrigger) && ((trigger as DamageTrigger).damageTriggerType == DamageTrigger.DamageTriggerType.Damaged)) {
           if (trigger.procTriggered is DamageProc) {
               ActivateDamageTrigger(attacker, defender, trigger);
           }
           else if (trigger.procTriggered is HealProc) {
               ActivateHealTrigger(attacker, defender, trigger);
           }
           else if (trigger.procTriggered is EffectProc) {
               ActivateEffectTrigger(attacker, defender, trigger);
           }
       } //end if DamageTrigger 
   } //end foreach

} //end CheckAttackerForTriggers


public void CheckDefenderForTriggers(BattleObject attacker, BattleObject defender, Proc proc) {

   foreach (ProcTrigger trigger in attacker.procTriggerList) {
       if ((trigger is DamageTrigger) && ((trigger as DamageTrigger).damageTriggerType == DamageTrigger.DamageTriggerType.WasDamaged)) {
           if (trigger.procTriggered is DamageProc) {
               ActivateDamageTrigger(defender, attacker, trigger);
           }
           else if (trigger.procTriggered is HealProc) {
               ActivateHealTrigger(defender, attacker, trigger);
           }
           else if (trigger.procTriggered is EffectProc) {
               ActivateEffectTrigger(defender, attacker, trigger);
           }
       } //end if DamageTrigger 
   } //end foreach

} //end CheckDefenderForTriggers



//EvadeTrigger functions

public void CheckEvaderForTriggers(BattleObject evader, BattleObject misser, Proc proc) {

   foreach (ProcTrigger trigger in evader.procTriggerList) {
       if ((trigger is EvadeTrigger) && ((trigger as EvadeTrigger).evadeTriggerType == EvadeTrigger.EvadeTriggerType.Evaded)) {
           if (trigger.procTriggered is DamageProc) {
               ActivateDamageTrigger(evader, misser, trigger);
           }
           else if (trigger.procTriggered is HealProc) {
               ActivateHealTrigger(evader, misser, trigger);
           }
           else if (trigger.procTriggered is EffectProc) {
               ActivateEffectTrigger(evader, misser, trigger);
           }     
       } //end if EvadeTrigger 
   } //end foreach

} //end CheckEvaderForTriggers


public void CheckMisserForTriggers(BattleObject evader, BattleObject misser, Proc proc) {

   foreach (ProcTrigger trigger in evader.procTriggerList) {
       if ((trigger is EvadeTrigger) && ((trigger as EvadeTrigger).evadeTriggerType == EvadeTrigger.EvadeTriggerType.WasEvaded)) {
           if (trigger.procTriggered is DamageProc) {
               ActivateDamageTrigger(misser, evader, trigger);
           }
           else if (trigger.procTriggered is HealProc) {
               ActivateHealTrigger(misser, evader, trigger);
           }
           else if (trigger.procTriggered is EffectProc) {
               ActivateEffectTrigger(misser, evader, trigger);
           }
       } //end if EvadeTrigger 
   } //end foreach

} //end CheckMisserForTriggers



//Blocktrigger functions

public void CheckBlockerForTriggers(BattleObject blocker, BattleObject blockee, Proc proc) {

   foreach (ProcTrigger trigger in blocker.procTriggerList) {
       if ((trigger is BlockTrigger) && ((trigger as BlockTrigger).blockTriggerType == BlockTrigger.BlockTriggerType.Blocked)) {
           if (trigger.procTriggered is DamageProc) {
               ActivateDamageTrigger(blocker, blockee, trigger);
           }
           else if (trigger.procTriggered is HealProc) {
               ActivateHealTrigger(blocker, blockee, trigger);
           }
           else if (trigger.procTriggered is EffectProc) {
               ActivateEffectTrigger(blocker, blockee, trigger);
           }
       } //end if BlockTrigger 
   } //end foreach

} //end CheckBlockerForTriggers


public void CheckBlockeeForTriggers(BattleObject blocker, BattleObject blockee, Proc proc) {

   foreach (ProcTrigger trigger in blocker.procTriggerList) {
       if ((trigger is BlockTrigger) && ((trigger as BlockTrigger).blockTriggerType == BlockTrigger.BlockTriggerType.WasBlocked)) {
           if (trigger.procTriggered is DamageProc) {
               ActivateDamageTrigger(blockee, blocker, trigger);
           }
           else if (trigger.procTriggered is HealProc) {
               ActivateHealTrigger(blockee, blocker, trigger);
           }
           else if (trigger.procTriggered is EffectProc) {
               ActivateEffectTrigger(blockee, blocker, trigger);
           }
       } //end if BlockTrigger 
   } //end foreach

} //end CheckBlockerForTriggers


*/
