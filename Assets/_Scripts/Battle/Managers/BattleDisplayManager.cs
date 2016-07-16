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

        SetRelativeSliderLengths();

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

        InitHeroDisplayController(heroDisplayController);

        heroDisplayControllerList.Add(heroDisplayController);
        
        return heroDisplayController;

    } // end CreateHeroDisplayPackage(1)


    private void InitHeroDisplayController(HeroDisplayController controller) {

        Debug.Log(controller.healthSlider.maxValue);
        controller.nameText.text = controller.hero.heroName;
        controller.healthSlider.maxValue = controller.hero.maxHealth;
        controller.manaSlider.maxValue = controller.hero.maxMana;
        Debug.Log(controller.healthSlider.maxValue);

    } // end InitHeroDisplay(1)
    

    private void UpdateHeroDisplayController(HeroDisplayController controller) {

        controller.healthText.text = (Mathf.Round(controller.hero.currentHealth)).ToString() + " / " + controller.hero.maxHealth.ToString();
        controller.manaText.text = (Mathf.Round(controller.hero.currentMana)).ToString() + " / " + controller.hero.maxMana.ToString();

        controller.healthSlider.value = (Mathf.Round(controller.hero.currentHealth));
        controller.manaSlider.value = (Mathf.Round(controller.hero.currentMana));
        
    } // end UpdateHeroDisplayController(1)


    public void SetRelativeSliderLengths() {
        
        List<float> valueList = new List<float>();

        foreach (HeroDisplayController controller in heroDisplayControllerList) {
            valueList.Add(controller.healthSlider.maxValue);
            valueList.Add(controller.manaSlider.maxValue);
        }

        float highestValue = Mathf.Max(valueList.ToArray());

        foreach (HeroDisplayController controller in heroDisplayControllerList) {
            SetControllerSliderScales(controller, highestValue);
        }
        
    } // end SetRelativeSliderLenghts
   

    private void SetControllerSliderScales(HeroDisplayController controller, float highestValue) {

        controller.healthSlider.transform.localScale = new Vector3((controller.healthSlider.maxValue / highestValue), 1, 1);
        controller.manaSlider.transform.localScale = new Vector3((controller.manaSlider.maxValue / highestValue), 1, 1);

    } // end SetControllerSliderScales()


} //end BattleDisplayManager()