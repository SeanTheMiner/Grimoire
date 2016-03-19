using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Abilities;
using System;

public class BattleDisplayManager : MonoBehaviour {

    public BattleManager battleManager;

    public Text heroOneNameText, heroTwoNameText,
        selectedHeroNameText,
        heroOneHealthText, heroTwoHealthText, 
        heroOneManaText, heroTwoManaText,
        abilityOneText, abilityTwoText, abilityThreeText, abilityFourText, abilityFiveText, abilitySixText,
        enemyOneHealthText, enemyTwoHealthText, enemyThreeHealthText
        ;

    public Slider heroOneHealthSlider, heroTwoHealthSlider,
        heroOneManaSlider, heroTwoManaSlider
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

        heroOneManaSlider.maxValue = battleManager.heroObjectOne.maxMana;
        heroTwoManaSlider.maxValue = battleManager.heroObjectTwo.maxMana;

    } //end Awake()


    public void InitNameText() {

        heroOneNameText.text = battleManager.heroObjectOne.heroName;
        heroTwoNameText.text = battleManager.heroObjectTwo.heroName;
    }


    public void UpdateHealthText() {

        heroOneHealthText.text = (Mathf.Round(battleManager.heroObjectOne.currentHealth)).ToString() + " / " + battleManager.heroObjectOne.maxHealth.ToString();
        heroTwoHealthText.text = (Mathf.Round(battleManager.heroObjectTwo.currentHealth)).ToString() + " / " + battleManager.heroObjectTwo.maxHealth.ToString();

        heroOneHealthSlider.value = (Mathf.Round(battleManager.heroObjectOne.currentHealth));
        heroTwoHealthSlider.value = (Mathf.Round(battleManager.heroObjectTwo.currentHealth));

        enemyOneHealthText.text = (Mathf.Round(battleManager.enemyObjectOne.currentHealth)).ToString() + " / " + battleManager.enemyObjectOne.maxHealth.ToString();
        enemyTwoHealthText.text = (Mathf.Round(battleManager.enemyObjectTwo.currentHealth)).ToString() + " / " + battleManager.enemyObjectTwo.maxHealth.ToString();
        enemyThreeHealthText.text = (Mathf.Round(battleManager.enemyObjectThree.currentHealth)).ToString() + " / " + battleManager.enemyObjectThree.maxHealth.ToString();

    }


    public void UpdateManaText() {

        heroOneManaText.text = (Mathf.Round(battleManager.heroObjectOne.currentMana)).ToString() + " / " + battleManager.heroObjectOne.maxMana.ToString();
        heroTwoManaText.text = (Mathf.Round(battleManager.heroObjectTwo.currentMana)).ToString() + " / " + battleManager.heroObjectTwo.maxMana.ToString();

        heroOneManaSlider.value = (Mathf.Round(battleManager.heroObjectOne.currentMana));
        heroTwoManaSlider.value = (Mathf.Round(battleManager.heroObjectTwo.currentMana));

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


    public void UpdateAbilityButtonText(Text text, Ability ability) {

        if (ability.cooldownEndTimer <= Time.time) {
            text.text = ability.abilityName;
        }
        else {
            text.text = (Mathf.Round(ability.cooldownEndTimer - Time.time)).ToString();
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
