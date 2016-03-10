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


    public void Initialize () {
        foreach (BattleObject host in affectedBattleObjectList)
        {
            SpawnDisplayObject(host);
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


    public IEnumerator CheckForExpiration(BattleObject host) {
        yield return new WaitForSeconds(effectApplied.effectDuration);

        GameObject effectToRemove = effectPrefabList[affectedBattleObjectList.IndexOf(host)];
        Destroy(effectToRemove);
        effectPrefabList.RemoveAt(affectedBattleObjectList.IndexOf(host));


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
