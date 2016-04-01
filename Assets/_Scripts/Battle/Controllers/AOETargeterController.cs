using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BattleObjects;

public class AOETargeterController : MonoBehaviour {

    private Vector3 mousePosition;
    public List<BattleObject> battleObjectList = new List<BattleObject>();

    private bool targeterPlaced = false;


    void Update () {

        if (!targeterPlaced) {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(mousePosition.x, 0.25f, mousePosition.z);
        }
      
	} //end Update()


    void OnTriggerEnter (Collider touchedCollider) {
       
        GameObject touchedObject = touchedCollider.gameObject;
        if (touchedObject.tag == "Enemy") {
            battleObjectList.Add(touchedObject.GetComponent<BattleObject>());
        }
    }


    void OnTriggerExit (Collider touchedCollider) {
        
        GameObject touchedObject = touchedCollider.gameObject;
        if(touchedObject.tag == "Enemy") {
            battleObjectList.Remove(touchedObject.GetComponent<BattleObject>());
        }
    }
    

    public void PlaceTargeter () {
        gameObject.GetComponent<Renderer>().enabled = false;
        targeterPlaced = true;
    }


} //end AOETargeterController
