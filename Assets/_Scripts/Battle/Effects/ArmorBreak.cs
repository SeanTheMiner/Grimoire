using UnityEngine;
using System.Collections;

using Effects;
using BattleObjects;

public class ArmorBreak : Effect {

	public ArmorBreak() {

        effectName = "Armor Break";
		effectDisplayText = "Armor down";
        effectIconText = "A-";
		effectDuration = 12;
        effectType = EffectType.Lump;
        statType = StatType.Physical;

    } //end Constructor

    public override void InitEffect(BattleObject host) {
        base.InitEffect(host);
        host.armorMultMod -= 0.65f;
    }

    public override void RemoveEffect(BattleObject host) {
		host.armorMultMod += 0.65f;
        base.RemoveEffect(host);
    }

} //end ArmorBreak class