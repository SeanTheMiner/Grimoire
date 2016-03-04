using UnityEngine;
using System.Collections;

using Effects;
using BattleObjects;

public class ArmorBreak : Effect {

	public ArmorBreak() {

        effectName = "Armor Break";
        effectDuration = 10;

    } //end Constructor

    public override void InitEffect(BattleObject host) {
        base.InitEffect(host);
        host.armor /= 2;
    }

    public override void RemoveEffect(BattleObject host) {
        base.RemoveEffect(host);
        host.armor *= 2;
    }

} //end ArmorBreak class