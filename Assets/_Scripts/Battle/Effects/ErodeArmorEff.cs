using UnityEngine;
using System.Collections;

using Effects;
using BattleObjects;

public class ErodeArmorEff : Effect {

    public ErodeArmorEff() {

        effectName = "Erode Armor";
        effectIconText = "A-";
        effectDuration = 13;
        effectType = EffectType.Lump;
        statType = StatType.Physical;
        
    } //end Constructor

    public override void InitEffect(BattleObject host) {
        base.InitEffect(host);
        host.armorAddMod -= 40;
    }

    public override void RemoveEffect(BattleObject host) {
        host.armorAddMod += 40;
        base.RemoveEffect(host);
    }

} //end SpiritBreak class