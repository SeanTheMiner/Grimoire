using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Heroes;
using Abilities;

public class AbilityButtonManager : MonoBehaviour {

    public Button abilityOneButton, abilityTwoButton, abilityThreeButton, abilityFourButton, abilityFiveButton, abilitySixButton;

    public Image cooldownMaskOne, cooldownMaskTwo, cooldownMaskThree, cooldownMaskFour, cooldownMaskFive, cooldownMaskSix,
        chargingMaskOne, chargingMaskTwo, chargingMaskThree, chargingMaskFour, chargingMaskFive, chargingMaskSix,
        durationMaskOne, durationMaskTwo, durationMaskThree, durationMaskFour, durationMaskFive, durationMaskSix
        ;

    
    public List<Button> buttonList = new List<Button>();
    public List<Image> cooldownMaskList = new List<Image>();
    public List<Image> chargingMaskList = new List<Image>();
    public List<Image> durationMaskList = new List<Image>();


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

        durationMaskList.Add(durationMaskOne);
        durationMaskList.Add(durationMaskTwo);
        durationMaskList.Add(durationMaskThree);
        durationMaskList.Add(durationMaskFour);
        durationMaskList.Add(durationMaskFive);
        durationMaskList.Add(durationMaskSix);

        ClearCooldownMasks();
        ClearChargingMasks();
        ClearDurationMasks();
        
    } //end Awake()



    public void UpdateSelectedHeroButtons (Hero hero) {

        CheckCooldownMask(hero.abilityOne, cooldownMaskOne);
        CheckCooldownMask(hero.abilityTwo, cooldownMaskTwo);
        CheckCooldownMask(hero.abilityThree, cooldownMaskThree);
        CheckCooldownMask(hero.abilityFour, cooldownMaskFour);
        CheckCooldownMask(hero.abilityFive, cooldownMaskFive);
        CheckCooldownMask(hero.abilitySix, cooldownMaskSix);

        if (hero.currentBattleState == Hero.BattleState.Charge) {
            
            CheckChargingMask(hero.abilityOne, chargingMaskOne);
            CheckChargingMask(hero.abilityTwo, chargingMaskTwo);
            CheckChargingMask(hero.abilityThree, chargingMaskThree);
            CheckChargingMask(hero.abilityFour, chargingMaskFour);
            CheckChargingMask(hero.abilityFive, chargingMaskFive);
            CheckChargingMask(hero.abilitySix, chargingMaskSix);
            
        } //end if charging
        else {
            ClearChargingMasks();
        }


        if (hero.currentBattleState == Hero.BattleState.Ability) {

            CheckDurationMask(hero.abilityOne, durationMaskOne);
            CheckDurationMask(hero.abilityTwo, durationMaskTwo);
            CheckDurationMask(hero.abilityThree, durationMaskThree);
            CheckDurationMask(hero.abilityFour, durationMaskFour);
            CheckDurationMask(hero.abilityFive, durationMaskFive);
            CheckDurationMask(hero.abilitySix, durationMaskSix);
            
        } //end if abilitying
        else {
            ClearDurationMasks();
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

        if (ability.chargeEndTimer > Time.time) {
            chargingMask.fillAmount = (1 - ((ability.chargeEndTimer - Time.time) / ability.chargeDuration));
        }
        else {
            chargingMask.fillAmount = 0;
        }
        
    } //end CheckCharging Mask(2)


    public void CheckDurationMask(Ability ability, Image durationMask) {

        if (ability.abilityEndTimer > Time.time) {
            durationMask.fillAmount = ((ability.abilityEndTimer - Time.time) / ability.abilityDuration);
        }
        else {
            durationMask.fillAmount = 0;
        }

    } //end CheckCharging Mask(2)




    public void ClearCooldownMasks() {
        foreach (Image cooldownMask in cooldownMaskList) {
            cooldownMask.fillAmount = 0;
        }
    }


    public void ClearChargingMasks() {
        foreach (Image chargingMask in chargingMaskList) {
            chargingMask.fillAmount = 0;
        }
    }


    public void ClearDurationMasks() {
        foreach (Image durationMask in durationMaskList) {
            durationMask.fillAmount = 0;
        }
    }



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
