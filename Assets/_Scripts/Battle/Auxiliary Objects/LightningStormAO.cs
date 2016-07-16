/*

using UnityEngine;
using System.Collections;

using AuxiliaryObjects;
using Procs;

public class LightningStormAO : AuxiliaryObject {

    public DamageProc damageProc = new DamageProc();

    public LightningStormAO() {

        objectDuration = 8;

        damageProc.procDamage = 50;
        damageProc.damageType = DamageProc.DamageType.Magical;
        damageProc.procSpacing = 0.8f;
        damageProc.magicalPenetration = 50;
        
    } // end Constructor()


    void Update() {

        if (damageProc.nextProcTimer <= Time.time) {

            DetermineHitOutcomeSingleAuxiliary((targetingManager.TargetRandomEnemy()), damageProc);
            damageProc.nextProcTimer = Time.time + damageProc.procSpacing;

        }

    } // end Update()


} // end LightningStormAO class

*/
