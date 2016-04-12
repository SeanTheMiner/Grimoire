using UnityEngine;
using System.Collections;

using Effects;
using BattleObjects;

public class InspireOffEffect : Effect {

    public InspireOffEffect() {

        effectName = "Inspire";
        effectDisplayText = "Armor Up";
        effectIconText = "P+";
        effectDuration = 8;
        effectType = EffectType.Lump;
        statType = StatType.Physical;

    } //end Constructor

    public override void InitEffect(BattleObject host) {
        base.InitEffect(host);
        host.physicalPenetrationAddMod += 50;
    }

    public override void RemoveEffect(BattleObject host) {
        host.physicalPenetrationAddMod -= 50;
        base.RemoveEffect(host);
    }

} //end SpiritBreak class