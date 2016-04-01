using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using BattleObjects;
using Effects;

public class EffectDisplayController : MonoBehaviour {

    public BattleObject displayHost;
    public Vector3 hostPosition;

    public float iconSpacing;
    public float iconOffset;
    public int iconMax;

    public List<GameObject> displayEffectIconList = new List<GameObject>();
    public List<Vector3> displayPositionList = new List<Vector3>();


	void Start () {

        iconSpacing = 0.25f;
        iconOffset = 0.1f;
        iconMax = 10;

        hostPosition = displayHost.transform.position;
        
        for (int i = 1; i <= iconMax; i++) {
            displayPositionList.Add(new Vector3((hostPosition.x + (i * (iconSpacing)) + iconOffset), (hostPosition.y + 0.5f), hostPosition.z));
        }
        
    } //end Start()
	
	// Update is called once per frame
	void Update () {

        //UpdateEffectIconPositions();

	}

    public void UpdateEffectIconPositions () {

        Debug.Log("update positions called");

        List<GameObject> tempList = new List<GameObject>();
        
        foreach (GameObject icon in displayEffectIconList) {
            if (icon == null) {
                tempList.Add(icon);
                Debug.Log("Adeded icon");
            }
        }

        displayEffectIconList = tempList;

        foreach (GameObject icon in displayEffectIconList) {
            icon.transform.position = displayPositionList[displayEffectIconList.IndexOf(icon)];
        }

    } //end UpdateEffectIconPositions()



}
