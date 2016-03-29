using UnityEngine;
using System.Collections;

public class ChangeStance : HeroAbility {

    public Champion ownerChampion;

	public ChangeStance() {

        abilityName = "Change Stance";
        abilityType = AbilityType.Toggle;
        targetScope = TargetScope.Untargeted;

        chargeDuration = 2;
        costsMana = false;
        hasCooldown = false;

        ownerChampion = (Champion)abilityOwner;
        
    } //end constructor

    public override void AbilityMap() {
        
        if (ownerChampion.currentStance == Champion.ChampionStance.Defensive) {
            ownerChampion.ChangeStanceToDefensive();
        }
        else {
            ownerChampion.ChangeStanceToOffensive();
        }
        
    } //end AbilityMap()
    
} //end ChangeStance class