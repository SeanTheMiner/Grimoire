using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using BattleObjects;
using Enemies;
using Heroes;
using Effects;

public class EffectDisplayController : MonoBehaviour {

    public BattleObject displayHost;
    public Vector3 hostPosition;

    public float enemyIconSpacing, enemyIconHorizontalOffset, enemyIconVerticalOffset,
        heroIconSpacing, heroIconHorizontalOffset, heroIconVerticalOffset,
        hoverSpacing;

    public int enemyIconRowMax, heroIconColumnMax;
    
    public List<GameObject> displayEffectIconList = new List<GameObject>();
    public List<Vector3> displayPositionList = new List<Vector3>();


	void Start () {

        enemyIconSpacing = 0.3f;
        enemyIconHorizontalOffset = 0.1f;
        enemyIconVerticalOffset = 0;
        enemyIconRowMax = 10;

        heroIconSpacing = 0.3f;
        heroIconHorizontalOffset = -0.75f;
        heroIconVerticalOffset = -0.85f;
        heroIconColumnMax = 5;

        hoverSpacing = 0.5f;

        hostPosition = displayHost.transform.position;
        displayHost.effectDisplayController = this;


        if (displayHost is Enemy) {

            for (int i = 1; i <= enemyIconRowMax; i++) {
                displayPositionList.Add(new Vector3((hostPosition.x + (i * (enemyIconSpacing)) + enemyIconHorizontalOffset), 
                    (hostPosition.y + hoverSpacing), 
                    (hostPosition.z + enemyIconVerticalOffset)));
            }

            for (int i = 1; i <= enemyIconRowMax; i++) {
                displayPositionList.Add(new Vector3((hostPosition.x + (i * (enemyIconSpacing)) + enemyIconHorizontalOffset), 
                    (hostPosition.y + hoverSpacing), 
                    (hostPosition.z + enemyIconVerticalOffset + enemyIconSpacing)));
            }

        } //end if Enemy

        if (displayHost is Hero) {

            for (int i = 1; i <= heroIconColumnMax; i++) {
                displayPositionList.Add(new Vector3((hostPosition.x + heroIconHorizontalOffset), 
                    (hostPosition.y + hoverSpacing), 
                    (hostPosition.z + (i * (heroIconSpacing)) + heroIconVerticalOffset)));
            }

            for (int i = 1; i <= heroIconColumnMax; i++) {
                displayPositionList.Add(new Vector3((hostPosition.x + heroIconHorizontalOffset + heroIconSpacing), 
                    (hostPosition.y + hoverSpacing), 
                    (hostPosition.z + (i * (heroIconSpacing)) + heroIconVerticalOffset)));
            }

        } //end if Hero
     
    } //end Start()


    public void UpdateEffectIconPositions () {

        foreach (GameObject icon in displayEffectIconList) {
            icon.transform.position = displayPositionList[displayEffectIconList.IndexOf(icon)];
        }

    } //end UpdateEffectIconPositions()
    

    public void KillAllEffects (BattleObject hostToRemove) {

        EffectManager effectManager = GameObject.Find("EffectManager").GetComponent<EffectManager>();
        
        foreach (EffectController effectController in effectManager.activeEffectControllerList) {
            effectController.KillHostControllerOfGivenHost(hostToRemove);
        }

    } //end KillAllEffects()

    
} //end EffectDisplayController class
