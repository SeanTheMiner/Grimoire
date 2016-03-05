using UnityEngine;
using System.Collections;

using Effects;
using BattleObjects;

public class ArmorBreak : Effect {

	public ArmorBreak() {

        effectName = "Armor Break";
		effectDisplayText = "ARM / 2";
		effectDuration = 5;

    } //end Constructor

    public override void InitEffect(BattleObject host) {
        base.InitEffect(host);
        effectedBattleObject.armor /= 2;
    }

    public override void RemoveEffect() {
		effectedBattleObject.armor *= 2;
        base.RemoveEffect();
    }

} //end ArmorBreak class