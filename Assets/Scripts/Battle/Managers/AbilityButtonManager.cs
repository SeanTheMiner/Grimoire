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
        infChargingMaskOne, infChargingMaskTwo, infChargingMaskThree, infChargingMaskFour, infChargingMaskFive, infChargingMaskSix,
        durationMaskOne, durationMaskTwo, durationMaskThree, durationMaskFour, durationMaskFive, durationMaskSix,
        infBarrageMaskOne, infBarrageMaskTwo, infBarrageMaskThree, infBarrageMaskFour, infBarrageMaskFive, infBarrageMaskSix,
        abilitySelecterImage
        ;
        


    public GameObject abilitySelecter;
    
    public List<Button> buttonList = new List<Button>();
    public List<Image> cooldownMaskList = new List<Image>();
    public List<Image> chargingMaskList = new List<Image>();
    public List<Image> infChargingMaskList = new List<Image>();
    public List<Image> durationMaskList = new List<Image>();
    public List<Image> infBarrageMaskList = new List<Image>();


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

        infChargingMaskList.Add(infChargingMaskOne);
        infChargingMaskList.Add(infChargingMaskTwo);
        infChargingMaskList.Add(infChargingMaskThree);
        infChargingMaskList.Add(infChargingMaskFour);
        infChargingMaskList.Add(infChargingMaskFive);
        infChargingMaskList.Add(infChargingMaskSix);

        durationMaskList.Add(durationMaskOne);
        durationMaskList.Add(durationMaskTwo);
        durationMaskList.Add(durationMaskThree);
        durationMaskList.Add(durationMaskFour);
        durationMaskList.Add(durationMaskFive);
        durationMaskList.Add(durationMaskSix);

        infBarrageMaskList.Add(infBarrageMaskOne);
        infBarrageMaskList.Add(infBarrageMaskTwo);
        infBarrageMaskList.Add(infBarrageMaskThree);
        infBarrageMaskList.Add(infBarrageMaskFour);
        infBarrageMaskList.Add(infBarrageMaskFive);
        infBarrageMaskList.Add(infBarrageMaskSix);

        ClearCooldownMasks();
        ClearChargingMasks();
        ClearInfChargingMasks();
        ClearDurationMasks();
        ClearInfBarrageMasks();

        abilitySelecterImage = abilitySelecter.GetComponent<Image>();
        abilitySelecterImage.enabled = false;
        
    } //end Awake()



    public void UpdateSelectedHeroButtons (Hero hero) {

        CheckCooldownMask(hero.abilityOne, cooldownMaskOne);
        CheckCooldownMask(hero.abilityTwo, cooldownMaskTwo);
        CheckCooldownMask(hero.abilityThree, cooldownMaskThree);
        CheckCooldownMask(hero.abilityFour, cooldownMaskFour);
        CheckCooldownMask(hero.abilityFive, cooldownMaskFive);
        CheckCooldownMask(hero.abilitySix, cooldownMaskSix);

        if (hero.currentAbility != null) {
            abilitySelecterImage.enabled = true;
            CheckAbilitySelecterPosition(hero);
        }
        else {
            abilitySelecterImage.enabled = false;
        }
        

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


        if ((hero.currentAbility != null) && (hero.currentAbility.abilityType == Ability.AbilityType.InfCharge)) {

            CheckInfChargingMask(hero.abilityOne, infChargingMaskOne);
            CheckInfChargingMask(hero.abilityTwo, infChargingMaskTwo);
            CheckInfChargingMask(hero.abilityThree, infChargingMaskThree);
            CheckInfChargingMask(hero.abilityFour, infChargingMaskFour);
            CheckInfChargingMask(hero.abilityFive, infChargingMaskFive);
            CheckInfChargingMask(hero.abilitySix, infChargingMaskSix);

        }
        else {
            ClearInfChargingMasks();
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
        

        if (hero.currentBattleState == Hero.BattleState.InfBarrage) {

            CheckInfBarrageMask(hero, hero.abilityOne, infBarrageMaskOne);
            CheckInfBarrageMask(hero, hero.abilityTwo, infBarrageMaskTwo);
            CheckInfBarrageMask(hero, hero.abilityThree, infBarrageMaskThree);
            CheckInfBarrageMask(hero, hero.abilityFour, infBarrageMaskFour);
            CheckInfBarrageMask(hero, hero.abilityFive, infBarrageMaskFive);
            CheckInfBarrageMask(hero, hero.abilitySix, infBarrageMaskSix);

        }
        else {
            ClearInfBarrageMasks();
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


    public void CheckInfChargingMask(Ability ability, Image infChargingMask) {

        if (ability.isInfCharging) {
            infChargingMask.fillAmount = ((Time.time - ability.infChargeStartTimer) - (Mathf.FloorToInt(Time.time - ability.infChargeStartTimer)));
        }
        else {
            infChargingMask.fillAmount = 0;
        }
    } //end CheckInfChargingMask(2)


    public void CheckDurationMask(Ability ability, Image durationMask) {

        if (ability.abilityEndTimer > Time.time) {
            durationMask.fillAmount = ((ability.abilityEndTimer - Time.time) / ability.abilityDuration);
        }
        else {
            durationMask.fillAmount = 0;
        }

    } //end CheckChargingMask(2)

    
    public void CheckInfBarrageMask(Hero hero, Ability ability, Image infBarrageMask) {
        
        if (hero.currentAbility == ability) {
            infBarrageMask.fillAmount = (1 - ((ability.nextProcTimer - Time.time) / ability.procSpacing));
        }
        else {
            infBarrageMask.fillAmount = 0;
        }
        
    } //end CheckInfBarrageMask(2)


    public void CheckAbilitySelecterPosition (Hero hero) {

        if (hero.currentAbility == hero.abilityOne) {
            abilitySelecter.transform.position = abilityOneButton.transform.position;
        }
        else if (hero.currentAbility == hero.abilityTwo) {
            abilitySelecter.transform.position = abilityTwoButton.transform.position;
        }
        else if (hero.currentAbility == hero.abilityThree) {
            abilitySelecter.transform.position = abilityThreeButton.transform.position;
        }
        else if (hero.currentAbility == hero.abilityFour) {
            abilitySelecter.transform.position = abilityFourButton.transform.position;
        }
        else if (hero.currentAbility == hero.abilityFive) {
            abilitySelecter.transform.position = abilityFiveButton.transform.position;
        }
        else if (hero.currentAbility == hero.abilitySix) {
            abilitySelecter.transform.position = abilitySixButton.transform.position;
        }

        SetAbilitySelecterColor(hero);


    } //end CheckAbilitySelecterPosition (Hero)


    public void SetAbilitySelecterColor (Hero hero) {

        if (hero.currentBattleState == Hero.BattleState.Target) {
            abilitySelecterImage.color = Color.red;
        }
        else if ((hero.currentBattleState == Hero.BattleState.Charge) | (hero.currentBattleState == Hero.BattleState.InfCharge)) {
            abilitySelecterImage.color = Color.green;
        }
        else if ((hero.currentBattleState == Hero.BattleState.Ability) | (hero.currentBattleState == Hero.BattleState.InfBarrage)) {
            abilitySelecterImage.color = Color.blue;
        }

    } //end SetAbilitySelecterColor (hero)




    //Clearing functions



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


    public void ClearInfChargingMasks() {
        foreach (Image infChargingMask in infChargingMaskList) {
            infChargingMask.fillAmount = 0;
        }
    }
    

    public void ClearDurationMasks() {
        foreach (Image durationMask in durationMaskList) {
            durationMask.fillAmount = 0;
        }
    }


    public void ClearInfBarrageMasks() {
        foreach (Image infBarrageMask in infBarrageMaskList) {
            infBarrageMask.fillAmount = 0;
        }
    }
    
} //end AbilityButtonManager class
