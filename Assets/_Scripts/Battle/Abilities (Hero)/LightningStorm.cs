using UnityEngine;
using System.Collections;

using Abilities;
using AuxiliaryObjects;
using Procs;

public class LightningStorm : HeroAbility {
    
    public LightningStorm() {

        abilityName = "Lightning Storm";
        abilityType = AbilityType.ObjectCreation;
        targetScope = TargetScope.Untargeted;
        abilityDamageType = AbilityDamageType.Magical;
        manaCost = 120;

        requiresTargeting = false;

        chargeDuration = 7;
        cooldownDuration = 25;

        auxiliaryObjectCreatedName = "LightningStormAO";

    } //End Constructor()


    public override void AbilityMap() {
        CreateAuxiliaryObject(auxiliaryObjectCreatedName);
        ExitAbility();
    }


} //End LightningStorm class