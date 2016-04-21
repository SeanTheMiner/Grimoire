using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DebugDisplayManager : MonoBehaviour {

    //Keep this

    public BattleManager battleManager;

    public Text textOne, textTwo, textThree, textFour,
        textFive, textSix, textSeven, textEight;

    public List<Text> textList = new List<Text>();

    //End Keep this
    //Do whatever you want with this 

    public Champion championObject;


    //end Do whatever you want with this


    public void InitDebugText() {

        textList.Add(textOne);
        textList.Add(textTwo);
        textList.Add(textThree);
        textList.Add(textFour);
        textList.Add(textFive);
        textList.Add(textSix);
        textList.Add(textSeven);
        textList.Add(textEight);
        
        textOne.text = battleManager.heroObjectThree.heroName;
        

    } //end InitDebugText()


    public void UpdateDebugText() {

        textOne.text = "E1S: " + battleManager.enemyObjectOne.spiritAddMod;
        textTwo.text = "E2S: " + battleManager.enemyObjectTwo.ApplyStatModifications(battleManager.enemyObjectTwo.spirit, battleManager.enemyObjectTwo.spiritMultMod, battleManager.enemyObjectTwo.spiritAddMod);
        textThree.text = "E3S: " + battleManager.enemyObjectThree.ApplyStatModifications(battleManager.enemyObjectThree.spirit, battleManager.enemyObjectThree.spiritMultMod, battleManager.enemyObjectThree.spiritAddMod); ;
        //textFour.text = "H4A: " + battleManager.heroObjectFour.ApplyStatModifications(battleManager.heroObjectFour.armor, battleManager.heroObjectFour.armorMultMod, battleManager.heroObjectFour.armorAddMod);

        //textFive.text = "H1S: " + battleManager.heroObjectOne.ApplyStatModifications(battleManager.heroObjectOne.spirit, battleManager.heroObjectOne.spiritMultMod, battleManager.heroObjectOne.spiritAddMod);
        //textSix.text = "H2S: " + battleManager.heroObjectTwo.ApplyStatModifications(battleManager.heroObjectTwo.spirit, battleManager.heroObjectTwo.spiritMultMod, battleManager.heroObjectTwo.spiritAddMod);
        //textSeven.text = "H3S: " + battleManager.heroObjectThree.ApplyStatModifications(battleManager.heroObjectThree.spirit, battleManager.heroObjectThree.spiritMultMod, battleManager.heroObjectThree.spiritAddMod);
        //textEight.text = "H4S: " + battleManager.heroObjectFour.ApplyStatModifications(battleManager.heroObjectFour.spirit, battleManager.heroObjectFour.spiritMultMod, battleManager.heroObjectFour.spiritAddMod);


        foreach (Text text in textList) {
            if (text.text == null) {
                text.text = "";
            }
        }


    } //end UpdateDebugText()
    

} //end DebugDisplayManager
