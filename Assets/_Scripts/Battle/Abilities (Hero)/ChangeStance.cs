using UnityEngine;
using System.Collections;

public class ChangeStance : HeroAbility {

    public Champion ownerChampion;
    public HeroManager heroManager;
    
    public ChangeStance() {

        abilityName = "Change Stance";
        abilityType = AbilityType.Toggle;
        targetScope = TargetScope.Untargeted;

        requiresTargeting = false;
        costsMana = false;
        canBeDefault = false;

        chargeDuration = 1;
        cooldownDuration = 4;
       
        ownerChampion = (Champion)abilityOwner;
        
    } //end Constructor()


    public override void AbilityMap() {

        if (ownerChampion.currentStance == Champion.ChampionStance.Defensive) {
            ownerChampion.ChangeStanceToOffensive();
        }
        else {
            ownerChampion.ChangeStanceToDefensive();
        }
        ExitAbility();

    } //end AbilityMap()


    void SetChampion() {
        heroManager = GameObject.Find("HeroManager").GetComponent<HeroManager>();
        ownerChampion = heroManager.activeChampion;
    }

} //end ChangeStance class