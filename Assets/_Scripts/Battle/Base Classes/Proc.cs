﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Effects;

namespace Procs {

    public class Proc {

        public bool isDependent {
            get; set;
        }

        public bool hasDependent {
            get; set;
        }

        public Proc dependentProc;

    } //end AbilityProc class


    public class DamageProc : Proc {

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


        public DamageProc() {

            isDependent = false;
            hasDependent = false;
            isEvadeable = true;
            isBlockable = true;
            canCrit = true;
            procStartDelay = 0;

        } //end Constructor()

    } //end DamageProc class


    public class HealProc : Proc {


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


        public HealProc() {

            isDependent = false;
            isRevive = false;
            procStartDelay = 0;

        } //end Constructor()

    } //end HealProc class


    public class EffectProc : Proc {

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


        public EffectProc() {

            isDependent = true;
            chanceToApply = 100;
            resolveScale = 1;
            stacksApplied = 1;

        } //end Constructor()

    } //end EffectProc class
    

} //end Procs namespace