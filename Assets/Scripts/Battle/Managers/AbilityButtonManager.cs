using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Heroes;
using Abilities;

public class AbilityButtonManager : MonoBehaviour {

    public Button abilityOneButton, abilityTwoButton, abilityThreeButton, abilityFourButton, abilityFiveButton, abilitySixButton;

    public Image cooldownMaskOne, cooldownMaskTwo, cooldownMaskThree, cooldownMaskFour, cooldownMaskFive, cooldownMaskSix,
        chargingMaskOne, chargingMaskTwo, chargingMaskThree, chargingMaskFour, chargingMaskFive, chargingMaskSix
        ;

    
    public List<Button> buttonList = new List<Button>();
    public List<Image> cooldownMaskList = new List<Image>();
    public List<Image> chargingMaskList = new List<Image>();


    void Awake () {

        buttonList.Add(abilityOneButton);
        buttonList.Add(abilityTwoButton);
        buttonList.Add(abilityThreeButton);
        buttonList.Add(abilityFourButton);
        buttonList.Add(abilityFiveButton);
        buttonList.Add(abilitySixButton);

        cooldownMaskList.Add(cooldownMaskOne);
        cooldownMaskList.Add(cooldownMaskTwo);
        cooldownMaskList.Add(cooldownMaskThree);
        cooldownMaskList.Add(cooldownMaskFour);
        cooldownMaskList.Add(cooldownMaskFive);
        cooldownMaskList.Add(cooldownMaskSix);

        chargingMaskList.Add(chargingMaskOne);
        chargingMaskList.Add(chargingMaskTwo);
        chargingMaskList.Add(chargingMaskThree);
        chargingMaskList.Add(chargingMaskFour);
        chargingMaskList.Add(chargingMaskFive);
        chargingMaskList.Add(chargingMaskSix);


        foreach (Image cooldownMask in cooldownMaskList) {
            cooldownMask.fillAmount = 0;
        }

        ClearChargingMasks();
        
    } //end Awake()



    public void UpdateSelectedHeroButtons (Hero hero) {

        CheckCooldownMask(hero.abilityOne, cooldownMaskOne);
        CheckCooldownMask(hero.abilityTwo, cooldownMaskTwo);
        CheckCooldownMask(hero.abilityThree, cooldownMaskThree);
        CheckCooldownMask(hero.abilityFour, cooldownMaskFour);
        CheckCooldownMask(hero.abilityFive, cooldownMaskFive);
        CheckCooldownMask(hero.abilitySix, cooldownMaskSix);

        if (hero.currentBattleState == Hero.BattleState.Charge) {

            if (hero.currentAbility == hero.abilityOne) {
                CheckChargingMask(hero.abilityOne, chargingMaskOne);
            }
            else if (hero.currentAbility == hero.abilityTwo) {
                CheckChargingMask(hero.abilityTwo, chargingMaskTwo);
            }
            else if (hero.currentAbility == hero.abilityThree) {
                CheckChargingMask(hero.abilityThree, chargingMaskThree);
            }
            else if (hero.currentAbility == hero.abilityFour) {
                CheckChargingMask(hero.abilityFour, chargingMaskFour);
            }
            else if (hero.currentAbility == hero.abilityFive) {
                CheckChargingMask(hero.abilityFive, chargingMaskFive);
            }
            else if (hero.currentAbility == hero.abilitySix) {
                CheckChargingMask(hero.abilitySix, chargingMaskSix);
            }
            
        } //end if charging
        else {
            ClearChargingMasks();
        }


    } //end UpdateSelectedHeroButtons (Hero)


    public void CheckCooldownMask(Ability ability, Image cooldownMask) {

        if (ability.cooldownEndTimer > Time.time) {
            cooldownMask.fillAmount = ((ability.cooldownEndTimer - Time.time) / ability.cooldownDuration);
        }
        else {
            cooldownMask.fillAmount = 0;
        }

    } //end CheckCooldownMask (2)


    public void CheckChargingMask(Ability ability, Image chargingMask) {

        chargingMask.fillAmount = (1- ((ability.chargeEndTimer - Time.time) / ability.chargeDuration));

    } //end CheckCharging Mask(2)

    
    public void ClearChargingMasks() {
        foreach (Image chargingMask in chargingMaskList) {
            chargingMask.fillAmount = 0;
        }
    }
    



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
