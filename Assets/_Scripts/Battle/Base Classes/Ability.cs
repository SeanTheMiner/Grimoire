using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using BattleObjects;
using Heroes;
using Enemies;
using Effects;


namespace Abilities {

    public abstract class Ability : MonoBehaviour {

        public TargetingManager targetingManager = new TargetingManager();
       
        public string abilityName {
            get; set;
        }

        //eventually delete this, since it'll be on a proc
        public Effect effectApplied;

        public AbilityProc primaryProc;


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


    public class AbilityProc : MonoBehaviour {

        public bool isDependent {
            get; set;
        }
        
        public bool hasDependent {
            get; set;
        }

        public AbilityProc dependentProc;

    } //end AbilityProc class

    
    public class DamageProc : AbilityProc {

        public enum DamageType {
            Physical,
            Magical,
            True
        }

        public DamageType damageType {
            get; set;
        }

        public bool isEvadeable {
            get; set;
        }

        public bool isBlockable {
            get; set;
        }

        public bool canCrit {
            get; set;
        }

        //stats

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


        //nuts & bolts

        public float procStartDelay {
            get; set;
        }
        
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


        public DamageProc () {

            isDependent = false;
            hasDependent = false;
            isEvadeable = true;
            isBlockable = true;
            canCrit = true;
            procStartDelay = 0;

        } //end Constructor()
        
    } //end DamageProc class


    public class HealProc : AbilityProc {


        public bool isRevive {
            get; set;
        }


        //stats

        public float critChance {
            get; set;
        }

        public float critMultiplier {
            get; set;
        }


        //nuts & bolts

        public float procStartDelay {
            get; set;
        }

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

        public float procHeal {
            get; set;
        }
        

        public HealProc () {

            isDependent = false;
            isRevive = false;
            procStartDelay = 0;

        } //end Constructor()

    } //end HealProc class


    public class EffectProc : AbilityProc {

        public Effect effectApplied;

        public float chanceToApply {
            get; set;
        }

        public float resolveScale {
            get; set;
        }
        
        public float stacksApplied {
            get; set;
        }


        public EffectProc () {

            isDependent = true;
            chanceToApply = 100;
            resolveScale = 1;
            stacksApplied = 1;

        } //end Constructor()

    } //end EffectProc class
    

} //end Ability namespace