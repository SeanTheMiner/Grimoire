using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Heroes;
using Abilities;

public class AbilityButtonManager : MonoBehaviour {

    public Button abilityOneButton, abilityTwoButton, abilityThreeButton, abilityFourButton, abilityFiveButton, abilitySixButton;
    public Image cooldownMaskOne, cooldownMaskTwo, cooldownMaskThree, cooldownMaskFour, cooldownMaskFive, cooldownMaskSix;

    
    public List<Button> buttonList = new List<Button>();
    public List<Image> cooldownMaskList = new List<Image>();


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
        

        foreach (Image cooldownMask in cooldownMaskList) {
            cooldownMask.fillAmount = 0;
        }


    }



    public void UpdateSelectedHeroButtons (Hero hero) {

        CheckCooldownMask(hero.abilityOne, cooldownMaskOne);
        CheckCooldownMask(hero.abilityTwo, cooldownMaskTwo);
        CheckCooldownMask(hero.abilityThree, cooldownMaskThree);
        CheckCooldownMask(hero.abilityFour, cooldownMaskFour);
        CheckCooldownMask(hero.abilityFive, cooldownMaskFive);
        CheckCooldownMask(hero.abilitySix, cooldownMaskSix);

    } //end UpdateSelectedHeroButtons (Hero)


    public void CheckCooldownMask(Ability ability, Image cooldownMask) {

        if (ability.cooldownEndTimer > Time.time) {
            cooldownMask.fillAmount = ((ability.cooldownEndTimer - Time.time) / ability.cooldownDuration);
        }
        else {
            cooldownMask.fillAmount = 0;
        }

    } //end CheckCooldownMask





    public void ClearGraphics (Image image) {
        
        image.enabled = false;

    }




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
