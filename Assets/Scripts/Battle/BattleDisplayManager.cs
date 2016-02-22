using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BattleDisplayManager : MonoBehaviour {

    public BattleManager battleManager;

    public Text enemyOneHealthText;
    public Text enemyTwoHealthText;
    public Text enemyThreeHealthText;

    public void UpdateHealthText() {

        enemyOneHealthText.text = battleManager.enemyObjectOne.currentHealth.ToString();
        enemyTwoHealthText.text = battleManager.enemyObjectTwo.currentHealth.ToString();
        enemyThreeHealthText.text = battleManager.enemyObjectThree.currentHealth.ToString();

    }


}
