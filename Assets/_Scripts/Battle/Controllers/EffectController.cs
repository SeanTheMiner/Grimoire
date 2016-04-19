using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using BattleObjects;
using Effects;

public class EffectController : MonoBehaviour {

    //GameObject effectManager = GameObject.Find("EffectManager");

    public Effect effectApplied {
        get; set;
    }

    public List<EffectStruct> effectStructList = new List<EffectStruct>();

    public struct EffectStruct {

        public BattleObject host;
        public GameObject effectIcon;
        public TextMesh iconTextMesh;
        public EffectDisplayController effectDisplayController;
        public float expirationTimer;
        public int stackCount;

    } //end effectStruct

  
    //public List<BattleObject> affectedBattleObjectList = new List<BattleObject>();
    //public List<int> stackCountList = new List<int>();
    //public List<GameObject> effectIconList = new List<GameObject>();



    void Update () {

        if (effectStructList.Count <= 0) {
            //effectManager.GetComponent<EffectManager>().activeEffectControllerList.Remove(this);
            Destroy(this);
        }

        foreach (EffectStruct effectStruct in effectStructList) {
            CheckForExpiration(effectStruct);
        }
        
    } //end Update()


    private void CheckForExpiration (EffectStruct effectStruct) {

        if (effectStruct.expirationTimer >= Time.time) {
            if (effectApplied.effectType == Effect.EffectType.Lump) {
                KillStruct(effectStruct);
            }
            else if (effectApplied.effectType == Effect.EffectType.Stacking) {
                UpdateStacks(effectStruct, -1);
            }
        }

    } //end CheckForExpiration (1)


    public void UpdateStacks (EffectStruct effectStruct, int stacksToApply) {

        effectStruct.stackCount += (stacksToApply);
        effectStruct.iconTextMesh.text = effectStruct.stackCount.ToString();

    } //end UpdateStacks (2)
  

    public void KillStruct(EffectStruct effectStruct) {

        effectStructList.Remove(effectStruct);
        effectApplied.RemoveEffect(effectStruct.host);
        effectStruct.effectDisplayController.displayEffectIconList.Remove(effectStruct.effectIcon);

        Destroy(effectStruct.effectIcon);
        effectStruct.effectDisplayController.UpdateEffectIconPositions();

    } //end KillEffect (1)
    

    public void InitStruct(BattleObject host, int initialStacks) {

        EffectStruct effectStruct = new EffectStruct();

        effectStruct.host = host;
        effectStruct.effectIcon = SpawnDisplayIcon(host);
        effectStruct.iconTextMesh = effectStruct.effectIcon.GetComponentInChildren<TextMesh>();
        effectStruct.effectDisplayController = host.GetComponentInChildren<EffectDisplayController>();
        effectStruct.expirationTimer = Time.time + ApplyTenacity(host, effectApplied.effectDuration);
        effectStruct.stackCount = initialStacks;

        effectStructList.Add(effectStruct);

    } //end InitStruct (1)

    /*
    public void InitStructMultiple (List<BattleObject> hostList, int initialStacks) {

        foreach (BattleObject host in hostList) {
            InitStruct(host, initialStacks);
        }

    } //end InitStructMultiple (2)
    */


    public GameObject SpawnDisplayIcon(BattleObject host) {

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

        } //end if Stacking

        effectDisplayController.displayEffectIconList.Add(effectIcon);
        
        return effectIcon;

    } //end SpawnDisplayIcon (1)


    public float ApplyTenacity(BattleObject host, float duration) {

        return (duration *= (1 - (host.ApplyStatModifications(host.tenacity, host.tenacityMultMod, host.tenacityAddMod) / 100)));

    } //end ApplyTenacity (2)






    /*

    public void KillEffect(BattleObject host, GameObject icon) {

        affectedBattleObjectList.Remove(host);
        effectApplied.RemoveEffect(host);

        EffectDisplayController effectDisplayController = host.GetComponentInChildren<EffectDisplayController>();

        effectDisplayController.displayEffectIconList.Remove(icon);
        Destroy(icon);
        effectDisplayController.UpdateEffectIconPositions();

    } //end KillEffect(2)


    public void Initialize () {

        if (effectApplied.effectType == Effect.EffectType.Lump) {
            foreach (BattleObject host in affectedBattleObjectList) {        
                StartCoroutine(CheckForExpirationLump(host, SpawnDisplayIcon(host)));
            }
        }
        else if (effectApplied.effectType == Effect.EffectType.Stacking) {
            foreach (BattleObject host in affectedBattleObjectList) {
                StartCoroutine(CheckForExpirationStacking(host, SpawnDisplayIcon(host), affectedBattleObjectList.IndexOf(host)));
            }
        }
        
	} //end Initialize
    

    public void InitializeMultipleStacking (List<BattleObject> passedList) {

        if (effectApplied.effectType == Effect.EffectType.Lump) {
            foreach (BattleObject host in passedList) {
                StartCoroutine(CheckForExpirationLump(host, SpawnDisplayIcon(host)));
            }
        }
        else if (effectApplied.effectType == Effect.EffectType.Stacking) {
   
            foreach (BattleObject host in passedList) {
                StartCoroutine(CheckForExpirationStacking(host, SpawnDisplayIcon(host), affectedBattleObjectList.IndexOf(host)));
            }
        }

    } //end Initialize

    //became InitStruct
    public void AddHostToExistingEffect (BattleObject host) {
        StartCoroutine(CheckForExpirationStacking(host, SpawnDisplayIcon(host), affectedBattleObjectList.IndexOf(host)));
    } //end AddHostToExistingEffect





    public IEnumerator CheckForExpirationLump(BattleObject host, GameObject icon) {
        
        yield return new WaitForSeconds(ApplyTenacity(host, effectApplied.effectDuration));
        KillEffect(host, icon);
        
    } //end CheckForExpiration()






    public IEnumerator CheckForExpirationStacking(BattleObject host, GameObject icon, int index) {
        
        icon.GetComponentInChildren<TextMesh>().text = stackCountList[index].ToString();
        
        //update stack effect here? Probably function on the effect that takes in old stacks, new stacks, removes effect of old and applies effect of new
       
        yield return new WaitForSeconds(ApplyTenacity(host, effectApplied.stackDuration));

        //effectApplied.stackCount--;
        //icon.GetComponentInChildren<TextMesh>().text = effectApplied.stackCount.ToString();
        
        stackCountList[index]--;
        icon.GetComponentInChildren<TextMesh>().text = stackCountList[index].ToString();

        if (stackCountList[index] > 0) {
            StartCoroutine(CheckForExpirationStacking(host, icon, index));
        }
        else {
            KillEffect(host, icon);
        }

    } //end CheckForExpiration(3)


    public void RestartCheckForExpirationStacking(BattleObject host) {

        int index = affectedBattleObjectList.IndexOf(host);
        GameObject icon = effectIconList[index];
        StartCoroutine(CheckForExpirationStacking(host, icon, index));

    } //end RestartCheckForExpirationStacking(1)



    */

   



} //end EffectController class
