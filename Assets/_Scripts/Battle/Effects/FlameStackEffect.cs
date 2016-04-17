using UnityEngine;
using System.Collections;

using Effects;
using BattleObjects;

public class FlameStackEffect : Effect {

    public FlameStackEffect() {

        effectName = "Flame Stack";
        effectIconText = "";
        stackDuration = 0.4f;
        isCoreEffect = true;
        effectType = EffectType.Stacking;
        statType = StatType.Magical;

    } //end Constructor ()


    public override void EffectMap(BattleObject host) {

        //eventually it updates stats for effect here
        //Calls UpdateStacks mostly? UpdateStacks should be on Effect, though

    }
  
    /*

    UpdateStacks (int old, int new) {
        remove effect with old number, reapply effect with new number
    }
    */

} //end FlameStackEffect class