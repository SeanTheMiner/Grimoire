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

            objectDuration = 10;

            damageProc.procDamage = 120;
            damageProc.damageType = DamageProc.DamageType.Magical;
            damageProc.procSpacing = 1;

            damageProc.magicalPenetration = 50;
            damageProc.magicalAccuracy = 50;
            damageProc.magicalFinesse = 50;
            damageProc.critChance = 30;
            damageProc.critMultiplier = 1.5f;

        } // End constructor()

        void Update() {
         
            if (damageProc.nextProcTimer <= Time.time) {

                DetermineHitOutcomeSingleAuxiliary((targetingManager.TargetRandomEnemy()), damageProc);
                damageProc.nextProcTimer = Time.time + damageProc.procSpacing;

            } // End if

        } // End Update()
        
    } // End LightningStormAO class


    public LightningStormAO lightningStormAO;

    public LightningStorm() {

        objectCreated = lightningStormAO;

        abilityName = "Lightning Storm";
        abilityType = AbilityType.ObjectCreation;
        targetScope = TargetScope.Untargeted;
        abilityDamageType = AbilityDamageType.Magical;
        manaCost = 120;

        requiresTargeting = false;

        chargeDuration = 4;
        cooldownDuration = 12;
       
        
    } //End Constructor()


    public override void AbilityMap() {

        AuxiliaryObject aO = FindAuxiliaryObjectManager().AddComponent<LightningStormAO>();
        aO.InitAuxiliaryObject();
        ExitAbility();

    } // End AbilityMap()


} //End LightningStorm class