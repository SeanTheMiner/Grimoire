using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Abilities;
using System;
using Heroes;

public class BattleDisplayManager : MonoBehaviour {


    public BattleManager battleManager;
    public GameObject heroDisplayPanel;

    public Vector3 displayOrigin;
    public float displaySpacing;
    
    public List<HeroDisplayController> heroDisplayControllerList = new List<HeroDisplayController>();
  

    public class HeroDisplayController : MonoBehaviour {

        public Hero hero;
        public Text nameText, healthText, manaText;
        public Slider healthSlider, manaSlider;

    } // end HeroDisplayPackage class


    public void Start() {
        
        displaySpacing = 25;
        displayOrigin = new Vector3 (heroDisplayPanel.transform.position.x, heroDisplayPanel.transform.position.y + displaySpacing);

        //note that the positioning should be figured to take count and i and figure out the spacing from the center based on both. I just suck at math.

        for (int i = 0; i < battleManager.heroList.Count; i++) {
            Vector3 position = new Vector3(displayOrigin.x, displayOrigin.y - (i * battleManager.heroList.Count * displaySpacing));
            heroDisplayControllerList.Add(CreateHeroDisplayPackage(battleManager.heroList[i], position));
        }

        //SetRelativeSliderLengths();

    } // end Start()


    void Update() {

        foreach (HeroDisplayController controller in heroDisplayControllerList) {
            UpdateHeroDisplayController(controller);
        } // end foreach
        
    } // end Update()


    public HeroDisplayController CreateHeroDisplayPackage(Hero hero, Vector3 position) {

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

        return heroDisplayController;

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


    */

} //end BattleDisplayManager()