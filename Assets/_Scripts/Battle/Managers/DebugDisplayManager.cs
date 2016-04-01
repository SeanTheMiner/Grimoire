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

       textTwo.text = championObject.currentStance.ToString();

        foreach (Text text in textList) {
            if (text.text == null) {
                text.text = "";
            }
        }


    } //end UpdateDebugText()
    

} //end DebugDisplayManager
