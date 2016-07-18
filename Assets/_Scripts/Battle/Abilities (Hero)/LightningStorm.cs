using UnityEngine;
using System.Collections;

using Abilities;
using AuxiliaryObjects;
using Procs;


public class LightningStorm : HeroAbility {

    public class LightningStormAO : AuxiliaryObject {

        public DamageProc damageProc = new DamageProc();

        public LightningStormAO () {

            sourceAbility = new LightningStorm();

            objectDuration = 8;

            damageProc.procDamage = 50;
            damageProc.damageType = DamageProc.DamageType.Magical;
            damageProc.procSpacing = 0.8f;
            damageProc.magicalPenetration = 50;

        } // end constructor()

        void Update() {

            Debug.Log("updated");


            if (damageProc.nextProcTimer <= Time.time) {

                DetermineHitOutcomeSingleAuxiliary((targetingManager.TargetRandomEnemy()), damageProc);
                damageProc.nextProcTimer = Time.time + damageProc.procSpacing;

            } // end if

        } // end Update()
        
    } // end LightningStormAO class

    public LightningStormAO lightningStormAO;

    public LightningStorm() {

        objectCreated = lightningStormAO;

        abilityName = "Lightning Storm";
        abilityType = AbilityType.ObjectCreation;
        targetScope = TargetScope.Untargeted;
        abilityDamageType = AbilityDamageType.Magical;
        manaCost = 120;

        requiresTargeting = false;

        chargeDuration = 1;
        cooldownDuration = 2;

        
        
    } //End Constructor()


    public override void AbilityMap() {

        FindAuxiliaryObjectManager().AddComponent<LightningStormAO>();
        ExitAbility();

    }


} //End LightningStorm class