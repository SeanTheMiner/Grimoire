using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using BattleObjects;

namespace Effects {

    public class Effect : MonoBehaviour {
        
       	public string effectName {
            get; set;
        }

        public string effectDisplayText {
            get; set;
        }

        public string effectIconText {
			get; set;
		}

        public float effectDuration {
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
        
        public float stackDuration {
            get; set;
        }


        public float resolveScale {
            get; set;
        }

        public enum StatType {
            Physical,
            Magical,
            None                
        }

        public StatType statType {
            get; set;
        }

        public enum EffectType {
            Lump,
            Stacking,
            Persistent
        }

        public EffectType effectType {
            get; set;
        }


        public Effect () {

            effectDuration = 0;
            canStack = false;
            stackCount = 0;
            resolveScale = 1;

        } //end constructor

        public GameObject effectManager;

		
		//Virtual functions, to be overridden on child classes as needed

		//I feel like most effects won't need a map.
		//But, for those that decay over time or do anything special,
		//orrr are triggered by events, I think they'll need that?
		//InvokeRepeating won't work for something that needs to check every frame.
		//Might need to research if there's a way to suppress update or something,
		//because if there is an effect script on a hero's ability like there needs to be,
		//it'll just get called all day.

        public virtual void EffectMap(BattleObject host) {}


        public virtual void InitEffect(BattleObject host) {
            host.effectList.Add(this);
        }


        public virtual void CreateEffectSingle(BattleObject host) {
            
            EffectController effectController = effectManager.AddComponent<EffectController>();
            effectController.effectApplied = this;
            effectController.affectedBattleObjectList.Add(host);
            effectController.Initialize();
            InitEffect(host);
           
        } //endCreateEffectSingle()


        public virtual void CreateEffectMultiple(List<BattleObject> list) {

            EffectController effectController = effectManager.AddComponent<EffectController>();
            effectController.effectApplied = this;
            foreach (BattleObject host in list)
            {
                effectController.affectedBattleObjectList.Add(host);
                InitEffect(host);
            }
            effectController.Initialize();
            
        } //end CreateEffectMultiple()


        public virtual void RemoveEffect(BattleObject host) {
			host.effectList.Remove (this);
        }


        public virtual void InitStacks(int stacks) {
            stackCount += stacks;
        }

        
    } //end Effect Class

} //end Effects namespace
