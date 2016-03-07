using UnityEngine;
using System.Collections;

using Effects;
using BattleObjects;

public class ArmorBreak : Effect {

	public ArmorBreak() {

        effectName = "Armor Break";
		effectDisplayText = "Armor down";
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

    public override void SpawnDisplayObject()
    {
        base.SpawnDisplayObject();
        effectDisplayPrefab.AddComponent<ArmorBreak>();
    }

} //end ArmorBreak class