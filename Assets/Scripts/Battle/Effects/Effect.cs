using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Heroes;
using Enemies;

namespace Effects {

    public class Effect : MonoBehaviour {

       public string effectName {
            get; set;
        }

        public float effectDuration {
            get; set;
        }

        public float effectStartTimer {
            get; set;
        }

        public bool canStack {
            get; set;
        }

        //this might just be counting the list? 
        //This is simpler if they're permanent or something.
        public int stackCount {
            get; set;
        }
        
        public float resolveScale {
            get; set;
        }

        public Hero effectedHero {
            get; set;
        }

        public Enemy effectedEnemy {
            get; set;
        }

        public Effect () {

            effectDuration = 0;
            effectStartTimer = 0;
            canStack = false;
            stackCount = 0;
            resolveScale = 1;

        } //end constructor
        

        public virtual void InitEffect() {
            effectStartTimer = Time.time;
            
        }

        public virtual void ResetEffect() {
            effectStartTimer = 0;
        }











    } //end Effect Class

} //end Effects namespace
