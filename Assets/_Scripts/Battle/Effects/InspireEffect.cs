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
        host.armorAddMod += 50;
    }

    public override void RemoveEffect(BattleObject host) {
        host.armorAddMod -= 50;
        base.RemoveEffect(host);
    }

} //end SpiritBreak class