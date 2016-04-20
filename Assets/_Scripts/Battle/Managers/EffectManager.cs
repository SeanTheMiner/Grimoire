using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Effects;

public class EffectManager : MonoBehaviour {

    public List<EffectController> activeEffectControllerList = new List<EffectController>();

    void Update () {

        activeEffectControllerList.RemoveAll(pancake => pancake == null);

    }
    
} //end EffectManager
