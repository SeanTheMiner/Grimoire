using UnityEngine;
using System.Collections;

using Effects;
using BattleObjects;

public class FlameStackEffect : Effect {

    public FlameStackEffect() {

        effectName = "Flame Stack";
        effectIconText = "";
        stackDuration = 1.4f;
        effectType = EffectType.Stacking;
        statType = StatType.Magical;

    } //end Constructor

    

    public override void EffectMap(BattleObject host) {
        
        //eventually it updates stats for effect here

    }

} //end ArmorBreak class