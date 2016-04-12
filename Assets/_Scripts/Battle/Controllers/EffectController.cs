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

        if (effectApplied.effectType == Effect.EffectType.Lump) {
            foreach (BattleObject host in affectedBattleObjectList) {        
                StartCoroutine(CheckForExpirationLump(host, SpawnDisplayIcon(host)));
            }
        }
        else if (effectApplied.effectType == Effect.EffectType.Stacking) {
            foreach (BattleObject host in affectedBattleObjectList) {
                //Could do something here
                StartCoroutine(CheckForExpirationStacking(host, SpawnDisplayIcon(host)));
            }
        }


	} //end Initialize
    

    public GameObject SpawnDisplayIcon (BattleObject host) {

        GameObject effectIcon = new GameObject();
        EffectDisplayController effectDisplayController = host.GetComponentInChildren<EffectDisplayController>();

        if (effectApplied.effectType == Effect.EffectType.Lump) {
            
            effectIcon = (GameObject)Instantiate(Resources.Load("EffectIconLump"),
                effectDisplayController.displayPositionList[effectDisplayController.displayEffectIconList.Count],
                Quaternion.identity
                );

            if (effectApplied.statType == Effect.StatType.Magical) {
                effectIcon.GetComponentInChildren<Renderer>().material =
                    Resources.Load("Magic Icon Material", typeof(Material)) as Material;
            }
            else if (effectApplied.statType == Effect.StatType.Physical) {
                effectIcon.GetComponentInChildren<Renderer>().material =
                    Resources.Load("Physical Icon Material", typeof(Material)) as Material;
            }

            effectIcon.GetComponentInChildren<TextMesh>().text = effectApplied.effectIconText;

        } //end if lump

        else if (effectApplied.effectType == Effect.EffectType.Stacking) {

            effectIcon = (GameObject)Instantiate(Resources.Load("EffectIconStacking"),
                effectDisplayController.displayPositionList[effectDisplayController.displayEffectIconList.Count],
                Quaternion.identity
                );

        } 
        
        effectDisplayController.displayEffectIconList.Add(effectIcon);
        effectIconList.Add(effectIcon);

        return effectIcon;

    } //end SpawnDisplayIcon



    public IEnumerator CheckForExpirationLump(BattleObject host, GameObject icon) {

        //Tenacity here?
        yield return new WaitForSeconds(effectApplied.effectDuration);
        KillEffect(host, icon);
        
    } //end CheckForExpiration()


    public IEnumerator CheckForExpirationStacking(BattleObject host, GameObject icon) {

        Debug.Log(effectApplied.stackCount.ToString());

        //Tenacity here?
        yield return new WaitForSeconds(effectApplied.stackDuration);

        effectApplied.stackCount--;
        icon.GetComponentInChildren<TextMesh>().text = effectApplied.stackCount.ToString();

        if (effectApplied.stackCount > 0) {
            StartCoroutine(CheckForExpirationStacking(host, icon));
        }
        else {
            KillEffect(host, icon);
        } //end else

    } //end CheckForExpiration()


    public void KillEffect(BattleObject host, GameObject icon) {

        affectedBattleObjectList.Remove(host);
        effectApplied.RemoveEffect(host);

        EffectDisplayController effectDisplayController = host.GetComponentInChildren<EffectDisplayController>();

        effectDisplayController.displayEffectIconList.Remove(icon);
        Destroy(icon);
        effectDisplayController.UpdateEffectIconPositions();

    } //end KillEffect(2)


    void Update () {
        if (affectedBattleObjectList.Count <= 0)
        {
           Destroy(this);
       }
    } //end Update()


} //end EffectController class
