using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugDisplayManager : MonoBehaviour {

    public BattleManager battleManager;

    public Text heroOneNameText;
    public Text heroTwoNameText;

    public Text heroOneBStateText;
    public Text heroTwoBStateText;

    public Text heroOneTarAbText;
    public Text heroTwoTarAbText;

    public Text heroOneCurAbText;
    public Text heroTwoCurAbText;

    public Text heroOneChargeText;
    public Text heroTwoChargeText;

    public Text debugChargeTimerText;


    public void InitDebugText() {

        heroOneNameText.text = battleManager.heroObjectOne.heroName;
        heroTwoNameText.text = battleManager.heroObjectTwo.heroName;

    } //end InitDebugText()


    public void UpdateDebugText() {

        heroOneBStateText.text = battleManager.heroObjectOne.currentBattleState.ToString();
        heroTwoBStateText.text = battleManager.heroObjectTwo.currentBattleState.ToString();

        if(battleManager.heroObjectOne.targetingAbility == null) {
            heroOneTarAbText.text = "No ability";
        }
        else {
            heroOneTarAbText.text = battleManager.heroObjectOne.targetingAbility.abilityName;
        }

        if(battleManager.heroObjectTwo.targetingAbility == null) {
            heroTwoTarAbText.text = "No ability";
        }
        else {
            heroTwoTarAbText.text = battleManager.heroObjectTwo.targetingAbility.abilityName;
        }


        if(battleManager.heroObjectOne.currentAbility == null) {
            heroOneCurAbText.text = "No ability";
        }
        else {
            heroOneCurAbText.text = battleManager.heroObjectOne.currentAbility.abilityName;
        }

        if(battleManager.heroObjectTwo.currentAbility == null) {
            heroTwoCurAbText.text = "No ability";
        }
        else {
            heroTwoCurAbText.text = battleManager.heroObjectTwo.currentAbility.abilityName;
        }

        if(battleManager.heroObjectOne.currentBattleState != Heroes.Hero.BattleState.Charge) {
            heroOneChargeText.text = "";
        }
        else {
            heroOneChargeText.text = (battleManager.heroObjectOne.currentAbility.chargeEndTimer - Time.time).ToString();
        }

        if(battleManager.heroObjectTwo.currentBattleState != Heroes.Hero.BattleState.Charge) {
            heroTwoChargeText.text = "";
        }
        else {
            heroTwoChargeText.text = (battleManager.heroObjectTwo.currentAbility.chargeEndTimer - Time.time).ToString();
        }

        if((battleManager.heroObjectTwo.currentBattleState == Heroes.Hero.BattleState.InfCharge) | (battleManager.heroObjectTwo.targetingAbility == battleManager.heroObjectTwo.abilityThree)) {
            debugChargeTimerText.text = (Time.time - battleManager.heroObjectTwo.abilityThree.infChargeStartTimer).ToString();
        }
        else {
            debugChargeTimerText.text = "";
        }

    } //end UpdateDebugText()
    

} //end DebugDisplayManager
