using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BattleObjects;

public class AOETargeterController : MonoBehaviour {

    private Vector3 mousePosition;
    public List<BattleObject> battleObjectList = new List<BattleObject>();


    void Update () {

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(mousePosition.x, 0.25f, mousePosition.z);
      
	} //end Update()

    void OnTriggerEnter (Collider touchedCollider) {
       
        Debug.Log("enemytouched");
        GameObject touchedObject = touchedCollider.gameObject;
        battleObjectList.Add(touchedObject.GetComponent<BattleObject>());
    }

    void OnTriggerExit (Collider touchedCollider) {

        Debug.Log("enemyleft");
        GameObject touchedObject = touchedCollider.gameObject;
        battleObjectList.Remove(touchedObject.GetComponent<BattleObject>());
    }



} //end AOETargeterController
