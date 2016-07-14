using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Abilities;
using System;
using Heroes;

public class BattleDisplayManager : MonoBehaviour {

    //AbilityButtonDisplayControllers have to be linked up in Start()




    public BattleManager battleManager;
    public GameObject heroDisplayPanel;

    public Vector3 displayOrigin;
    public float displaySpacing;

    public List<Text> abilityTextList = new List<Text>();

    public Text selectedHeroNameText,
        abilityOneText, abilityTwoText, abilityThreeText, abilityFourText, abilityFiveText, abilitySixText,
        abilityOneManaText, abilityTwoManaText, abilityThreeManaText, abilityFourManaText, abilityFiveManaText, abilitySixManaText;

    public List<HeroDisplayController> heroDisplayControllerList = new List<HeroDisplayController>();

    public AbilityButtonDisplayController abilityOneController, abilityTwoController, abilityThreeController, abilityFourController, abilityFiveController, abilitySixController;

    public class HeroDisplayController : MonoBehaviour {

        public Hero hero;
        public Text nameText, healthText, manaText;
        public Slider healthSlider, manaSlider;

    } // end HeroDisplayPackage class

    
    public class AbilityButtonDisplayController : MonoBehaviour {

        public HeroAbility ability;
        public Text abilityNameText, manaCostText;

    } // end AbilityButtonDisplayController class


    public void Start() {


        displaySpacing = 25;
        displayOrigin = new Vector3 (heroDisplayPanel.transform.position.x, heroDisplayPanel.transform.position.y + displaySpacing);

        //note that the positioning should be figured to take count and i and figure out the spacing from the center based on both. I just suck at math.

        for (int i = 0; i < battleManager.heroList.Count; i++) {
            Vector3 position = new Vector3(displayOrigin.x, displayOrigin.y - (i * battleManager.heroList.Count * displaySpacing));
            CreateHeroDisplayPackage(battleManager.heroList[i], position);
        }

        

        
        PopulateAbilityTextList();
        //SetRelativeSliderLengths();

    } // end Start()


    public void CreateHeroDisplayPackage(Hero hero, Vector3 position) {

        GameObject heroDisplay = (GameObject)MonoBehaviour.Instantiate(Resources.Load("HeroDisplay"),
            position,
            Quaternion.identity
            );

        heroDisplay.transform.SetParent(heroDisplayPanel.transform);

        HeroDisplayController heroDisplayController = heroDisplay.AddComponent<HeroDisplayController>();

        heroDisplayController.hero = hero;

        Text[] textComponents = heroDisplay.GetComponentsInChildren<Text>();
        heroDisplayController.nameText = textComponents[0];
        heroDisplayController.healthText = textComponents[1];
        heroDisplayController.manaText = textComponents[2];

        Slider[] sliderComponents = heroDisplay.GetComponentsInChildren<Slider>();
        heroDisplayController.healthSlider = sliderComponents[0];
        heroDisplayController.manaSlider = sliderComponents[1];

        heroDisplayControllerList.Add(heroDisplayController);
       
    } // end CreateHeroDisplayPackage(1)


    private void InitHeroDisplayController(HeroDisplayController controller) {

        controller.nameText.text = controller.hero.heroName;
        controller.healthSlider.maxValue = controller.hero.maxHealth;
        controller.manaSlider.maxValue = controller.hero.maxMana;
        
    } // end InitHeroDisplay(1)



    private void UpdateHeroDisplayController(HeroDisplayController controller) {

        controller.healthText.text = (Mathf.Round(controller.hero.currentHealth)).ToString() + " / " + controller.hero.maxHealth.ToString();
        controller.manaText.text = (Mathf.Round(controller.hero.currentMana)).ToString() + " / " + controller.hero.maxMana.ToString();

        controller.healthSlider.value = (Mathf.Round(controller.hero.currentHealth));
        controller.manaSlider.value = (Mathf.Round(controller.hero.currentMana));

    } // end UpdateHeroDisplayController(1)


    public void UpdateAbilityButtonText(AbilityButtonDisplayController controller) {

        if (controller.ability.cooldownEndTimer > Time.time) {
            controller.abilityNameText.text = (Mathf.CeilToInt(controller.ability.cooldownEndTimer - Time.time)).ToString();
        }
        else if (controller.ability.chargeEndTimer > Time.time) {
            controller.abilityNameText.text = (Mathf.CeilToInt(controller.ability.chargeEndTimer - Time.time)).ToString();
        }
        else if (controller.ability.abilityEndTimer > Time.time) {
            controller.abilityNameText.text = (Mathf.CeilToInt(controller.ability.abilityEndTimer - Time.time)).ToString();
        }
        else if (controller.ability.isInfCharging) {
            controller.abilityNameText.text = (Mathf.FloorToInt(Time.time - controller.ability.infChargeStartTimer)).ToString();
        }
        else {
            controller.abilityNameText.text = controller.ability.abilityName;
        }

        controller.manaCostText.text = controller.ability.manaCost.ToString();

        if (controller.ability.manaCost >= battleManager.selectedHero.currentMana) {
            controller.manaCostText.color = Color.red;
        }
        else {
            controller.manaCostText.color = Color.black;
        }

    } // end UpdateAbilityButtonText(1)

    
    private void PopulateAbilityTextList() {

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

    } // end AddAbilityTexts()


    public void UpdateSelectedHeroText() {

        if (battleManager.selectedHero != null) {

            selectedHeroNameText.text = battleManager.selectedHero.heroName;
            
            UpdateAbilityButtonText(abilityOneController);
            UpdateAbilityButtonText(abilityTwoController);
            UpdateAbilityButtonText(abilityThreeController);
            UpdateAbilityButtonText(abilityFourController);
            UpdateAbilityButtonText(abilityFiveController);
            UpdateAbilityButtonText(abilitySixController);

        } // end if there is a selected hero
        else {
            NoHeroSelected();
        }

    } // end UpdateSelectedHeroText()
    

    public void NoHeroSelected() {

        selectedHeroNameText.text = "Select hero.";

        foreach (Text abilityText in abilityTextList) {
            abilityText.text = "";
        }

    } //end NoHeroSelected()


    /*

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


    */

} //end BattleDisplayManager()