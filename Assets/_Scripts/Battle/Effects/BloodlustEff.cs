using UnityEngine;
using System.Collections;
using Effects;
using BattleObjects;

public class BloodlustEff : Effect {

    public BloodlustEff() {

        effectName = "Bloodlust";
        effectIconText = "BL";
        effectDuration = 12;
        statType = StatType.None;
        effectType = EffectType.Lump;
        isStun = true;
        
    } //end Constructor()


    public override void InitEffect(BattleObject host) {
        base.InitEffect(host);
        host.physicalAttackSpeedMultMod += 0.5f;
        host.physicalLifeStealAddMod += 12;
    }

    public override void RemoveEffect(BattleObject host) {
        host.physicalAttackSpeedMultMod -= 0.5f;
        host.physicalLifeStealAddMod -= 12;
        base.RemoveEffect(host);
    }
    

} //end BloodlustEff class
