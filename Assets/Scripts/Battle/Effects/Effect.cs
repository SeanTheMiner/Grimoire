using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using BattleObjects;

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

        public BattleObject effectedBattleObject {
            get; set;
        }

        public Effect () {

            effectDuration = 0;
            effectStartTimer = 0;
            canStack = false;
            stackCount = 0;
            resolveScale = 1;

        } //end constructor
        
        public virtual void EffectMap(BattleObject host) {
            CheckForExpiration(host);
        }


        public virtual void InitEffect(BattleObject host) {
            effectStartTimer = Time.time;
            host.effectList.Add(this);

            //Spawn prefab as child of enemy (panel eventually)

        }

        public virtual void RemoveEffect(BattleObject host) {
            effectStartTimer = 0;
        }

        public virtual void CheckForExpiration(BattleObject host) {
            if ((effectStartTimer + effectDuration) >= Time.time) {
                RemoveEffect(host);
            }
        }









    } //end Effect Class

} //end Effects namespace
