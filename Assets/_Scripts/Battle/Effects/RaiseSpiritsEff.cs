using UnityEngine;
using System.Collections;

using Effects;
using BattleObjects;

public class RaiseSpiritsEff : Effect {

    public RaiseSpiritsEff() {

        effectName = "Raise Spirits";
        effectDisplayText = "Spirit Up";
        effectIconText = "S+";
        effectDuration = 10;
        effectType = EffectType.Lump;
        statType = StatType.Magical;

    } //end Constructor

    public override void InitEffect(BattleObject host) {
        base.InitEffect(host);
        host.spiritMultMod += 0.7f;
    }

    public override void RemoveEffect(BattleObject host) {
        host.spiritMultMod -= 0.7f;
        base.RemoveEffect(host);
    }

} //end SpiritBreak class