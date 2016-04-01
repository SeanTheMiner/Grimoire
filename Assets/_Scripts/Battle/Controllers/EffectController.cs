using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using BattleObjects;
using Effects;

public class EffectController : MonoBehaviour {


    public Effect effectApplied {
        get; set;
    }

    public List<BattleObject> affectedBattleObjectList = new List<BattleObject>();
    public List<GameObject> effectPrefabList = new List<GameObject>();
    public List<GameObject> effectIconList = new List<GameObject>();


    public void Initialize () {
        foreach (BattleObject host in affectedBattleObjectList)
        {
            //SpawnDisplayObject(host);
            SpawnDisplayIcon(host);
            StartCoroutine(CheckForExpiration(host));
        }
	} //end Initialize


    public void SpawnDisplayObject(BattleObject host) {
        GameObject effectPrefab = new GameObject();
        Vector3 spawnPosition = host.transform.position;

        effectPrefab = (GameObject)Instantiate(Resources.Load("EffectPrefab"),
                                             spawnPosition,
                                             Quaternion.Euler(90, 0, 0)
                                         );

        TextMesh displayTextMesh = effectPrefab.GetComponentInChildren<TextMesh>();
        displayTextMesh.text = effectApplied.effectDisplayText;
        effectPrefabList.Add(effectPrefab);
    }


    public void SpawnDisplayIcon (BattleObject host) {

        GameObject effectIcon = new GameObject();

        EffectDisplayController effectDisplayController = host.GetComponentInChildren<EffectDisplayController>();

        effectIcon = (GameObject)Instantiate(Resources.Load("EffectIcon"),
            effectDisplayController.displayPositionList[effectDisplayController.displayEffectIconList.Count],
            Quaternion.identity
            );

        effectIcon.GetComponentInChildren<TextMesh>().text = effectApplied.effectIconText;

        if (effectApplied.statType == Effect.StatType.Magical) {
            effectIcon.GetComponentInChildren<Renderer>().material = 
                Resources.Load("Magic Icon Material", typeof(Material)) as Material;
        } 
        else if (effectApplied.statType == Effect.StatType.Physical) {
            effectIcon.GetComponentInChildren<Renderer>().material =
                Resources.Load("Physical Icon Material", typeof(Material)) as Material;
        }

        effectDisplayController.displayEffectIconList.Add(effectIcon);
        effectIconList.Add(effectIcon);

    } //end SpawnDisplayIcon



    public IEnumerator CheckForExpiration(BattleObject host) {
        yield return new WaitForSeconds(effectApplied.effectDuration);

        /*

        GameObject effectToRemove = effectPrefabList[affectedBattleObjectList.IndexOf(host)];
        Destroy(effectToRemove);
        effectPrefabList.RemoveAt(affectedBattleObjectList.IndexOf(host));

    */

        GameObject iconToRemove = effectIconList[affectedBattleObjectList.IndexOf(host)];
        Destroy(iconToRemove);

        
        
        effectIconList.RemoveAt(affectedBattleObjectList.IndexOf(host));

        //host.GetComponentInChildren<EffectDisplayController>().UpdateEffectIconPositions();

        affectedBattleObjectList.Remove(host);
        effectApplied.RemoveEffect(host);

    } //end CheckForExpiration()


    void Update () {
        if (affectedBattleObjectList.Count <= 0)
        {
           Destroy(this);
       }
    } //end Update()


} //end EffectController class
