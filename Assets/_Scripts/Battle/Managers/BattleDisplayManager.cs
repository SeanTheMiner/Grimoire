using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Abilities;
using System;
using Heroes;

public class BattleDisplayManager : MonoBehaviour {

    public BattleManager battleManager;

    public Text heroOneNameText, heroTwoNameText, heroThreeNameText, heroFourNameText,
        selectedHeroNameText,
        heroOneHealthText, heroTwoHealthText, heroThreeHealthText, heroFourHealthText, 
        heroOneManaText, heroTwoManaText, heroThreeManaText, heroFourManaText,
        abilityOneText, abilityTwoText, abilityThreeText, abilityFourText, abilityFiveText, abilitySixText,
        abilityOneManaText, abilityTwoManaText, abilityThreeManaText, abilityFourManaText, abilityFiveManaText, abilitySixManaText,
        enemyOneHealthText, enemyTwoHealthText, enemyThreeHealthText
        ;

    public Slider heroOneHealthSlider, heroTwoHealthSlider,
        heroOneManaSlider, heroTwoManaSlider,
        heroThreeHealthSlider, heroThreeManaSlider,
        heroFourHealthSlider, heroFourManaSlider
        ;

    //public Animator anim;
    //Animator anim?
    //in awake anim = GetComponent <Animator>();

    public List<Text> abilityTextList = new List<Text>();

    public void Awake () {

        abilityTextList.Add(abilityOneText);
        abilityTextList.Add(abilityTwoText);
        abilityTextList.Add(abilityThreeText);
        abilityTextList.Add(abilityFourText);
        abilityTextList.Add(abilityFiveText);
        abilityTextList.Add(abilitySixText);

        abilityTextList.Add(abilityOneManaText);
        abilityTextList.Add(abilityTwoManaText);
        abilityTextList.Add(abilityThreeManaText);
        abilityTextList.Add(abilityFourManaText);
        abilityTextList.Add(abilityFiveManaText);
        abilityTextList.Add(abilitySixManaText);

        heroOneHealthSlider.maxValue = battleManager.heroObjectOne.maxHealth;
        heroTwoHealthSlider.maxValue = battleManager.heroObjectTwo.maxHealth;
        heroThreeHealthSlider.maxValue = battleManager.heroObjectThree.maxHealth;
        heroFourHealthSlider.maxValue = battleManager.heroObjectFour.maxHealth;

        heroOneManaSlider.maxValue = battleManager.heroObjectOne.maxMana;
        heroTwoManaSlider.maxValue = battleManager.heroObjectTwo.maxMana;
        heroThreeManaSlider.maxValue = battleManager.heroObjectThree.maxMana;
        heroFourManaSlider.maxValue = battleManager.heroObjectFour.maxMana;

        SetRelativeSliderLengths();

    } //end Awake()


    public void InitNameText() {

        heroOneNameText.text = battleManager.heroObjectOne.heroName;
        heroTwoNameText.text = battleManager.heroObjectTwo.heroName;
        heroThreeNameText.text = battleManager.heroObjectThree.heroName;
        heroFourNameText.text = battleManager.heroObjectFour.heroName;
        
    } //end InitNameText()


    public void SetRelativeSliderLengths() {

        //really this needs to check for if a mana bar is the highest bar. I don't really expect that to happen, but yeah.
        //Otherwise, frankly, the League system of having ticks every 100 could work...
        //Or vertical health bars?

        float highestMaxValue = heroOneHealthSlider.maxValue;
        int highestHeroPosition = 1;

        if (heroTwoHealthSlider.maxValue > highestMaxValue) {
            highestMaxValue = heroTwoHealthSlider.maxValue;
            highestHeroPosition = 2;
        }
        if (heroThreeHealthSlider.maxValue > highestMaxValue) {
            highestMaxValue = heroThreeHealthSlider.maxValue;
            highestHeroPosition = 3;
        }
        if (heroFourHealthSlider.maxValue > highestMaxValue) {
            highestMaxValue = heroFourHealthSlider.maxValue;
            highestHeroPosition = 4;
        }

        if (highestHeroPosition == 1) {
            SetBarLength(heroTwoHealthSlider, highestMaxValue);
            SetBarLength(heroThreeHealthSlider, highestMaxValue);
            SetBarLength(heroFourHealthSlider, highestMaxValue);
        }
        else if (highestHeroPosition == 2) {
            SetBarLength(heroOneHealthSlider, highestMaxValue);
            SetBarLength(heroThreeHealthSlider, highestMaxValue);
            SetBarLength(heroFourHealthSlider, highestMaxValue);
        }
        else if (highestHeroPosition == 3) {
            SetBarLength(heroOneHealthSlider, highestMaxValue);
            SetBarLength(heroTwoHealthSlider, highestMaxValue);
            SetBarLength(heroFourHealthSlider, highestMaxValue);
        }
        else if (highestHeroPosition == 4) {
            SetBarLength(heroOneHealthSlider, highestMaxValue);
            SetBarLength(heroTwoHealthSlider, highestMaxValue);
            SetBarLength(heroThreeHealthSlider, highestMaxValue);
        }

        SetBarLength(heroOneManaSlider, highestMaxValue);
        SetBarLength(heroTwoManaSlider, highestMaxValue);
        SetBarLength(heroThreeManaSlider, highestMaxValue);
        SetBarLength(heroFourManaSlider, highestMaxValue);
        
    } //end SetRelativeSliderLengths()

    
    public void SetBarLength(Slider slider, float highestValue) {
        slider.transform.localScale = new Vector3((slider.maxValue / highestValue), 1, 1);
    } //end SetBarLength(2)


    public void UpdateHealthText() {

        heroOneHealthText.text = (Mathf.Round(battleManager.heroObjectOne.currentHealth)).ToString() + " / " + battleManager.heroObjectOne.maxHealth.ToString();
        heroTwoHealthText.text = (Mathf.Round(battleManager.heroObjectTwo.currentHealth)).ToString() + " / " + battleManager.heroObjectTwo.maxHealth.ToString();
        heroThreeHealthText.text = (Mathf.Round(battleManager.heroObjectThree.currentHealth)).ToString() + " / " + battleManager.heroObjectThree.maxHealth.ToString();
        heroFourHealthText.text = (Mathf.Round(battleManager.heroObjectFour.currentHealth)).ToString() + " / " + battleManager.heroObjectFour.maxHealth.ToString();

        heroOneHealthSlider.value = (Mathf.Round(battleManager.heroObjectOne.currentHealth));
        heroTwoHealthSlider.value = (Mathf.Round(battleManager.heroObjectTwo.currentHealth));
        heroThreeHealthSlider.value = (Mathf.Round(battleManager.heroObjectThree.currentHealth));
        heroFourHealthSlider.value = (Mathf.Round(battleManager.heroObjectFour.currentHealth));

        enemyOneHealthText.text = (Mathf.Round(battleManager.enemyObjectOne.currentHealth)).ToString() + " / " + battleManager.enemyObjectOne.maxHealth.ToString();
        enemyTwoHealthText.text = (Mathf.Round(battleManager.enemyObjectTwo.currentHealth)).ToString() + " / " + battleManager.enemyObjectTwo.maxHealth.ToString();
        enemyThreeHealthText.text = (Mathf.Round(battleManager.enemyObjectThree.currentHealth)).ToString() + " / " + battleManager.enemyObjectThree.maxHealth.ToString();

    } //end UpdateHealthText()


    public void UpdateManaText() {

        heroOneManaText.text = (Mathf.Round(battleManager.heroObjectOne.currentMana)).ToString() + " / " + battleManager.heroObjectOne.maxMana.ToString();
        heroTwoManaText.text = (Mathf.Round(battleManager.heroObjectTwo.currentMana)).ToString() + " / " + battleManager.heroObjectTwo.maxMana.ToString();
        heroThreeManaText.text = (Mathf.Round(battleManager.heroObjectThree.currentMana)).ToString() + " / " + battleManager.heroObjectThree.maxMana.ToString();
        heroFourManaText.text = (Mathf.Round(battleManager.heroObjectFour.currentMana)).ToString() + " / " + battleManager.heroObjectFour.maxMana.ToString();

        heroOneManaSlider.value = (Mathf.Round(battleManager.heroObjectOne.currentMana));
        heroTwoManaSlider.value = (Mathf.Round(battleManager.heroObjectTwo.currentMana));
        heroThreeManaSlider.value = (Mathf.Round(battleManager.heroObjectThree.currentMana));
        heroFourManaSlider.value = (Mathf.Round(battleManager.heroObjectFour.currentMana));

    } //end UpdateManaText()


    public void UpdateSelectedHeroText() {
        
        if(battleManager.selectedHero != null) {

            selectedHeroNameText.text = battleManager.selectedHero.heroName;

            UpdateAbilityButtonText(abilityOneText, abilityOneManaText, battleManager.selectedHero.abilityOne);
            UpdateAbilityButtonText(abilityTwoText, abilityTwoManaText, battleManager.selectedHero.abilityTwo);
            UpdateAbilityButtonText(abilityThreeText, abilityThreeManaText, battleManager.selectedHero.abilityThree);
            UpdateAbilityButtonText(abilityFourText, abilityFourManaText, battleManager.selectedHero.abilityFour);
            UpdateAbilityButtonText(abilityFiveText, abilityFiveManaText, battleManager.selectedHero.abilityFive);
            UpdateAbilityButtonText(abilitySixText, abilitySixManaText, battleManager.selectedHero.abilitySix);
            
        } //end if there is a selected hero
        else {
            NoHeroSelected();
        }

    } //end UpdateSelectedHeroText()


    public void UpdateAbilityButtonText(Text abilityNameText, Text manaCostText, HeroAbility ability) {

        if (ability.cooldownEndTimer > Time.time) {
            abilityNameText.text = (Mathf.CeilToInt(ability.cooldownEndTimer - Time.time)).ToString();
        }
        else if (ability.chargeEndTimer > Time.time) {
            abilityNameText.text = (Mathf.CeilToInt(ability.chargeEndTimer - Time.time)).ToString();
        }
        else if (ability.abilityEndTimer > Time.time) {
            abilityNameText.text = (Mathf.CeilToInt(ability.abilityEndTimer - Time.time)).ToString();
        }
        else if (ability.isInfCharging) {
            abilityNameText.text = (Mathf.FloorToInt(Time.time - ability.infChargeStartTimer)).ToString();
        }
        else {
            abilityNameText.text = ability.abilityName;
        }

        manaCostText.text = ability.manaCost.ToString();
        ApplyManaCostColor(battleManager.selectedHero, ability, manaCostText);

    } //end UpdateAbilityButtonText(2)

    
    public void NoHeroSelected() {

        selectedHeroNameText.text = "Select hero.";

        foreach (Text abilityText in abilityTextList) {
            abilityText.text = "";
        }
        
    } //end NoHeroSelected()


    public void CheckForEnemyHealthRemoval() {
        if (battleManager.enemyObjectOne == null) {
            Destroy(enemyOneHealthText);
        }
        if(battleManager.enemyObjectTwo == null) {
            Destroy(enemyTwoHealthText);
        }
        if(battleManager.enemyObjectThree == null) {
            Destroy(enemyThreeHealthText);
        }
    } //end CheckForEnemyHealthRemoval()


    public void ApplyManaCostColor (Hero hero, HeroAbility ability, Text manaText) {

        if (ability.manaCost >= hero.currentMana) {
            manaText.color = Color.red;
        }
        else {
            manaText.color = Color.black;
        }

    } //end ApplyManaCostColor(3)

} //end BattleDisplayManager()
