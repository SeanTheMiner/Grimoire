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

    public void initHealthText() {

        heroOneNameText.text = battleManager.heroObjectOne.heroName;
        heroTwoNameText.text = battleManager.heroObjectTwo.heroName;
    }

    public void UpdateHealthText() {

        heroOneHealthText.text = battleManager.heroObjectOne.currentHealth.ToString();
        heroTwoHealthText.text = battleManager.heroObjectTwo.currentHealth.ToString();

        enemyOneHealthText.text = battleManager.enemyObjectOne.currentHealth.ToString();
        enemyTwoHealthText.text = battleManager.enemyObjectTwo.currentHealth.ToString();
        enemyThreeHealthText.text = battleManager.enemyObjectThree.currentHealth.ToString();

    }

    public void UpdateSelectedHeroText() {

        selectedHeroNameText.text = battleManager.selectedHero.heroName;
        abilityOneText.text = battleManager.selectedHero.abilityOne.abilityName;
        abilityTwoText.text = battleManager.selectedHero.abilityTwo.abilityName;

    }

    public void NoHeroSelected() {

        selectedHeroNameText.text = "Press 1 or 2 to select a hero.";
        abilityOneText.text = "";
        abilityTwoText.text = "";

    }

}
