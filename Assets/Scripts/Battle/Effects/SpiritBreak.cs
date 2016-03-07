using UnityEngine;
using System.Collections;

using Effects;
using BattleObjects;

public class SpiritBreak : Effect
{

    public SpiritBreak()
    {

        effectName = "Spirit Break";
        effectDisplayText = "Spirit down";
        effectDuration = 5;

    } //end Constructor

    public override void InitEffect(BattleObject host)
    {
        base.InitEffect(host);
        effectedBattleObject.spirit /= 2;
    }

    public override void RemoveEffect()
    {
        effectedBattleObject.spirit *= 2;
        base.RemoveEffect();
    }

    public override void SpawnDisplayObject()
    {
        base.SpawnDisplayObject();
        effectDisplayPrefab.AddComponent<SpiritBreak>();
    }

} //end SpiritBreak class