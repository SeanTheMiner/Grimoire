using UnityEngine;
using System.Collections;

using Effects;
using BattleObjects;

public class InspireEffect : Effect {

    public InspireEffect() {

        effectName = "Inspire";
        effectDisplayText = "Armor Up";
        effectIconText = "A+";
        effectDuration = 8;
        effectType = EffectType.Lump;
        statType = StatType.Physical;

    } //end Constructor

    public override void InitEffect(BattleObject host) {
        base.InitEffect(host);
        host.armor *= 2.5f;
    }

    public override void RemoveEffect(BattleObject host) {
        host.spirit /= 2.5f;
        base.RemoveEffect(host);
    }

} //end SpiritBreak class