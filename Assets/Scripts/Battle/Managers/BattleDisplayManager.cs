using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Abilities;
using System;

public class BattleDisplayManager : MonoBehaviour {

    public BattleManager battleManager;

    public Text heroOneNameText, heroTwoNameText, heroThreeNameText, heroFourNameText,
        selectedHeroNameText,
        heroOneHealthText, heroTwoHealthText, heroThreeHealthText, heroFourHealthText, 
        heroOneManaText, heroTwoManaText, heroThreeManaText, heroFourManaText,
        abilityOneText, abilityTwoText, abilityThreeText, abilityFourText, abilityFiveText, abilitySixText,
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

        heroOneHealthSlider.maxValue = battleManager.heroObjectOne.maxHealth;
        heroTwoHealthSlider.maxValue = battleManager.heroObjectTwo.maxHealth;
        heroThreeHealthSlider.maxValue = battleManager.heroObjectThree.maxHealth;
        heroFourHealthSlider.maxValue = battleManager.heroObjectFour.maxHealth;

        heroOneManaSlider.maxValue = battleManager.heroObjectOne.maxMana;
        heroTwoManaSlider.maxValue = battleManager.heroObjectTwo.maxMana;
        heroThreeManaSlider.maxValue = battleManager.heroObjectThree.maxMana;
        heroFourManaSlider.maxValue = battleManager.heroObjectFour.maxMana;

    } //end Awake()


    public void InitNameText() {

        heroOneNameText.text = battleManager.heroObjectOne.heroName;
        heroTwoNameText.text = battleManager.heroObjectTwo.heroName;
        heroThreeNameText.text = battleManager.heroObjectThree.heroName;
        heroFourNameText.text = battleManager.heroObjectFour.heroName;


    }


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

    }


    public void UpdateManaText() {

        heroOneManaText.text = (Mathf.Round(battleManager.heroObjectOne.currentMana)).ToString() + " / " + battleManager.heroObjectOne.maxMana.ToString();
        heroTwoManaText.text = (Mathf.Round(battleManager.heroObjectTwo.currentMana)).ToString() + " / " + battleManager.heroObjectTwo.maxMana.ToString();
        heroThreeManaText.text = (Mathf.Round(battleManager.heroObjectThree.currentMana)).ToString() + " / " + battleManager.heroObjectThree.maxMana.ToString();
        heroFourManaText.text = (Mathf.Round(battleManager.heroObjectFour.currentMana)).ToString() + " / " + battleManager.heroObjectFour.maxMana.ToString();

        heroOneManaSlider.value = (Mathf.Round(battleManager.heroObjectOne.currentMana));
        heroTwoManaSlider.value = (Mathf.Round(battleManager.heroObjectTwo.currentMana));
        heroThreeManaSlider.value = (Mathf.Round(battleManager.heroObjectThree.currentMana));
        heroFourManaSlider.value = (Mathf.Round(battleManager.heroObjectFour.currentMana));

    }


    public void UpdateSelectedHeroText() {
        
        if(battleManager.selectedHero != null) {

            selectedHeroNameText.text = battleManager.selectedHero.heroName;

            UpdateAbilityButtonText(abilityOneText, battleManager.selectedHero.abilityOne);
            UpdateAbilityButtonText(abilityTwoText, battleManager.selectedHero.abilityTwo);
            UpdateAbilityButtonText(abilityThreeText, battleManager.selectedHero.abilityThree);
            UpdateAbilityButtonText(abilityFourText, battleManager.selectedHero.abilityFour);
            UpdateAbilityButtonText(abilityFiveText, battleManager.selectedHero.abilityFive);
            UpdateAbilityButtonText(abilitySixText, battleManager.selectedHero.abilitySix);


        } //end if there is a selected hero
        else {
            NoHeroSelected();
        }

    } //end UpdateSelectedHeroText()


    public void UpdateAbilityButtonText(Text text, HeroAbility ability) {

        if (ability.cooldownEndTimer > Time.time) {
            text.text = (Mathf.CeilToInt(ability.cooldownEndTimer - Time.time)).ToString();
        }
        else if (ability.chargeEndTimer > Time.time) {
            text.text = (Mathf.CeilToInt(ability.chargeEndTimer - Time.time)).ToString();
        }
        else if (ability.abilityEndTimer > Time.time) {
            text.text = (Mathf.CeilToInt(ability.abilityEndTimer - Time.time)).ToString();
        }
        else if (ability.isInfCharging) {
            text.text = (Mathf.FloorToInt(Time.time - ability.infChargeStartTimer)).ToString();
        }
        else {
            text.text = ability.abilityName;
        }

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

} //end BattleDisplayManager()
