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
            StartCoroutine(CheckForExpiration(host, SpawnDisplayIcon(host)));
        }
	} //end Initialize
    

    public GameObject SpawnDisplayIcon (BattleObject host) {

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

        return effectIcon;

    } //end SpawnDisplayIcon



    public IEnumerator CheckForExpiration(BattleObject host, GameObject icon) {

        //Tenacity here?
        yield return new WaitForSeconds(effectApplied.effectDuration);

        affectedBattleObjectList.Remove(host);
        effectApplied.RemoveEffect(host);

        EffectDisplayController effectDisplayController = host.GetComponentInChildren<EffectDisplayController>();

        effectDisplayController.displayEffectIconList.Remove(icon);
        Destroy(icon);
        effectDisplayController.UpdateEffectIconPositions();
        
    } //end CheckForExpiration()


    void Update () {
        if (affectedBattleObjectList.Count <= 0)
        {
           Destroy(this);
       }
    } //end Update()


} //end EffectController class
