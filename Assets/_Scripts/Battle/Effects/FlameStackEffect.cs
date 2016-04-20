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
  
    /*

    ApplyStackEffect (int initialStacks) {

    }


    UpdateStackEffect (int old, int new) {
        remove effect with old number, reapply effect with new number
    }


    RemoveStackEffect (int remainingStacks) {

    }


    */

    


} //end FlameStackEffect class