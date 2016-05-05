using UnityEngine;
using System.Collections;
using Effects;

public class ThickSkinEff : Effect {

    public ThickSkinEff () {

        effectName = "Thick Skin";
        isDamageReduction = true;
        hasIcon = false;
        effectType = EffectType.Persistent;
        statType = StatType.None;

    } //end Constructor()

    public override float ApplyDamageReduction(float passedDamage) {
        return (0.75f * passedDamage);
    }

    
} //end ThickSkinEff