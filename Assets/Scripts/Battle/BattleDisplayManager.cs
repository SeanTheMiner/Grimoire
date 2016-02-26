using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BattleDisplayManager : MonoBehaviour {

    public BattleManager battleManager;

    public Text heroOneNameText;
    public Text heroTwoNameText;

    public Text heroOneHealthText;
    public Text heroTwoHealthText;

    public Text enemyOneHealthText;
    public Text enemyTwoHealthText;
    public Text enemyThreeHealthText;

    public Text selectedHeroNameText;
    public Text abilityOneText;
    public Text abilityTwoText;

    public void InitNameText() {

        heroOneNameText.text = battleManager.heroObjectOne.heroName;
        heroTwoNameText.text = battleManager.heroObjectTwo.heroName;
    }

    public void UpdateHealthText() {

        heroOneHealthText.text = battleManager.heroObjectOne.currentHealth.ToString() + "  /  " + battleManager.heroObjectOne.maxHealth.ToString();
        heroTwoHealthText.text = battleManager.heroObjectTwo.currentHealth.ToString() + "  /  " + battleManager.heroObjectTwo.maxHealth.ToString();

        enemyOneHealthText.text = battleManager.enemyObjectOne.currentHealth.ToString() + "  /  " + battleManager.enemyObjectOne.maxHealth.ToString();
        enemyTwoHealthText.text = battleManager.enemyObjectTwo.currentHealth.ToString() + "  /  " + battleManager.enemyObjectTwo.maxHealth.ToString();
        enemyThreeHealthText.text = battleManager.enemyObjectThree.currentHealth.ToString() + "  /  " + battleManager.enemyObjectThree.maxHealth.ToString();

    }

    public void UpdateSelectedHeroText() {

        if(battleManager.selectedHero != null) {

            selectedHeroNameText.text = battleManager.selectedHero.heroName;

            if(battleManager.selectedHero.abilityOne.cooldownEndTimer <= Time.time) {
                abilityOneText.text = battleManager.selectedHero.abilityOne.abilityName;
            }
            else {
                abilityOneText.text = (Mathf.Round(battleManager.selectedHero.abilityOne.cooldownEndTimer - Time.time)).ToString();
            }

            if(battleManager.selectedHero.abilityTwo.cooldownEndTimer <= Time.time) {
                abilityTwoText.text = battleManager.selectedHero.abilityTwo.abilityName;
            }
            else {
                abilityTwoText.text = (Mathf.Round(battleManager.selectedHero.abilityTwo.cooldownEndTimer - Time.time)).ToString();
            }

        } //end if there is a selected hero
        else {
            NoHeroSelected();
        }

    } //end UpdateSelectedHeroText()

    public void NoHeroSelected() {

        selectedHeroNameText.text = "Press 1 or 2 to select a hero.";
        abilityOneText.text = "";
        abilityTwoText.text = "";

    }

}
