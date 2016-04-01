using UnityEngine;
using System.Collections;

public class ChangeStance : HeroAbility {

    public Champion ownerChampion;

	public ChangeStance() {

        abilityName = "Change Stance";
        abilityType = AbilityType.Toggle;
        targetScope = TargetScope.Untargeted;
        requiresTargeting = false;

        chargeDuration = 1;
        cooldownDuration = 3;
        costsMana = false;
        

        ownerChampion = (Champion)abilityOwner;
        
    } //end constructor

    public override void AbilityMap() {

        if (ownerChampion.currentStance == Champion.ChampionStance.Defensive) {
            ownerChampion.ChangeStanceToOffensive();
        }
        else {
            ownerChampion.ChangeStanceToDefensive();
        }
        ExitAbility();

    } //end AbilityMap()
    
} //end ChangeStance class