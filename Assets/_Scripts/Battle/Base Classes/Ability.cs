using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using BattleObjects;
using Heroes;
using Enemies;
using Effects;
using Procs;


namespace Abilities {

    public abstract class Ability {

        public TargetingManager targetingManager = new TargetingManager();
       
        public string abilityName {
            get; set;
        }

        //eventually delete this, since it'll be on a proc
        public Effect effectApplied;

        public Proc primaryProc;


        public int effectStacksApplied {
            get; set;
        }

        public DamageType primaryDamageType {
            get; set;
        }

        public enum DamageType {
            Physical,
            Magical,
            Healing,
            None
        }

        
        //Targeting
        
        public Enemy targetEnemy {
            get; set;
        }

        public Hero targetHero {
            get; set;
        }

        public List<BattleObject> targetBattleObjectList = new List<BattleObject>();


        //Timekeeping
        
        public float chargeDuration {
            get; set;
        }

        public float chargeEndTimer {
            get; set;
        }

        public float abilityDuration {
            get; set;
        }

        public float abilityEndTimer {
            get; set;
        }

        public float cooldownDuration {
            get; set;
        }

        public float cooldownEndTimer {
            get; set;
        }



        //Proc handlers 
            //NOTE: useful for single-proc-type abilities, which will be MOST of them, 
            //but abilities with multiple procs will have to create their own variables 
            //and reflect this in the AbilityMap().

        public float nextProcTimer {
            get; set;
        }

        public int procCounter {
            get; set;
        }

        public int procLimit {
            get; set;
        }

        public float procSpacing {
            get; set;
        }

        public float procDamage {
            get; set;
        }

        public float procHeal {
            get; set;
        }


        //HitManager variables


        public float critChance {
            get; set;
        }

        public float critMultiplier {
            get; set;
        }

        public float physicalPenetration {
            get; set;
        }

        public float magicalPenetration {
            get; set;
        }

        public float physicalAccuracy {
            get; set;
        }

        public float magicalAccuracy {
            get; set;
        }

        public float physicalFinesse {
            get; set;
        }

        public float magicalFinesse {
            get; set;
        }
        
    } //end Ability class
 

} //end Abilities namespace