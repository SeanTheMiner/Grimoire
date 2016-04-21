using UnityEngine;
using System.Collections;

using Effects;
using BattleObjects;

public class FlameStackEffect : Effect {

    public FlameStackEffect() {

        effectName = "Flame Stack";
        effectIconText = "";
        stackDuration = 0.5f;
        isCoreEffect = true;
        effectType = EffectType.Stacking;
        statType = StatType.Magical;

    } //end Constructor ()


    public override void EffectMap(BattleObject host) {

        //eventually it updates stats for effect here
        //Calls UpdateStacks mostly? UpdateStacks should be on Effect, though

    }


    public override void InitEffectPerStack(BattleObject host, int stacks) {
        host.spiritAddMod -= (0.75f * stacks);
    }


    public override void RemoveEffectPerStack(BattleObject host, int stacks) {
        host.spiritAddMod += (0.75f * stacks);
    }
    
} //end FlameStackEffect class