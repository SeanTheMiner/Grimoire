using UnityEngine;
using System.Collections;

namespace Abilities {

    public class Ability : MonoBehaviour {


        //Defining bools? List of procs?

        //Timers & durations

        public string abilityName {
            get; set;
        }

        public float chargeDuration {
            get; set;
        }

        public float chargeStartTimer {
            get; set;
        }

        public float abilityDuration {
            get; set;
        }

        public float abilityStartTimer {
            get; set;
        }

        public float cooldown {
            get; set;
        }

        public float cooldownEndTimer {
            get; set;
        }


        //Proc handlers


        public float lastProcTimer {
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


        //Other stuff


        public string resource {
            get; set;
        }

        public int cost {
            get; set;
        }

        public TargetScope targetScope {
            get; set;
        }

        public enum TargetScope {
            Null,
            Untargeted,
            SingleHero,
            SingleEnemy,
            SingleHeroOrEnemy,
            AllHeroes,
            AllEnemies,
            AllHeroesOrAllEnemies,
            FreeTargetAOE
        }

        public DamageType primaryDamageType {
            get; set;
        }

        public enum DamageType {
            Null,
            Physical,
            Magical,
            Healing
        }

        public Ability() {

            chargeDuration = 0.0f;
            chargeStartTimer = 0.0f;
            abilityDuration = 0.0f;
            abilityStartTimer = 0.0f;
            cooldown = 0.0f;
            cooldownEndTimer = 0.0f;
            lastProcTimer = 0.0f;
            procCounter = 0;
            procLimit = 0;
            procSpacing = 0.0f;
            procDamage = 0.0f;
            procHeal = 0.0f;
            resource = "";
            cost = 0;
            targetScope = TargetScope.Null;
            primaryDamageType = DamageType.Null;

        } //end constructor

    } //end Ability class

} //end Ability namespace