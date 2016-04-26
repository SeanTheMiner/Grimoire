﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using BattleObjects;

namespace Effects {

    public class Effect {

        public GameObject effectManager;

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

        public bool isCoreEffect {
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
            isCoreEffect = false;
            stackCount = 0;
            resolveScale = 1;

        } //end constructor

        //public GameObject effectManager = GameObject.FindGameObjectWithTag("EffectManager");

		
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
        

        public virtual void RemoveEffect(BattleObject host) {
            host.effectList.Remove(this);
        }


        public virtual void InitTrigger(BattleObject host, ProcTriggers.ProcTrigger procTrigger) {
            procTrigger.triggerOwner = host;
            host.procTriggerList.Add(procTrigger);
        }


        public virtual void RemoveTrigger(BattleObject host, ProcTriggers.ProcTrigger procTrigger) {
            host.procTriggerList.Remove(procTrigger);
        }


        public virtual void InitEffectPerStack (BattleObject host, int stacks) {}
        public virtual void RemoveEffectPerStack(BattleObject host, int stacks) { }

        public virtual void UpdateEffectPerStack (BattleObject host, int currentStacks, int newStacks) {
            RemoveEffectPerStack(host, currentStacks);
            InitEffectPerStack(host, newStacks);
        } 
        

        public virtual void CreateEffectSingle(BattleObject host) {

            effectManager = GameObject.Find("EffectManager");
            EffectController effectController = effectManager.AddComponent<EffectController>();
            effectManager.GetComponent<EffectManager>().activeEffectControllerList.Add(effectController);

            effectController.effectApplied = this;
            effectController.InitHostController(host);

            InitEffect(host);
           
        } //endCreateEffectSingle()


        public virtual void CreateEffectMultiple(List<BattleObject> list) {
            
            effectManager = GameObject.Find("EffectManager");
            EffectController effectController = effectManager.AddComponent<EffectController>();
            effectManager.GetComponent<EffectManager>().activeEffectControllerList.Add(effectController);

            effectController.effectApplied = this;
            foreach (BattleObject host in list) {
                effectController.InitHostController(host);
                InitEffect(host);
            }
            
        } //end CreateEffectMultiple()


        public virtual void CreateStackingEffectSingle(BattleObject host, int stacksApplied) {

            effectManager = GameObject.Find("EffectManager");
            EffectController effectController;
            bool effectCreated = false;

            if (CheckForExistingEffectController(effectManager, this) == null) {
                effectController = effectManager.AddComponent<EffectController>();
                effectController.effectApplied = this;
                effectManager.GetComponent<EffectManager>().activeEffectControllerList.Add(effectController);
                effectController.InitHostControllerStacking(host, stacksApplied);
            }
            else {
                effectController = CheckForExistingEffectController(effectManager, this);
                effectCreated = true;
            }

            if (effectCreated) {
                foreach (EffectController.HostController hostController in effectController.hostControllerList) {
                    if (hostController.host == host) {
                        effectController.UpdateStacks(hostController, stacksApplied);
                        return;
                    }
                } //end foreach - only gets to here if host was not found

                //if you get here, it means the effectController was previously created, but the host was not found
                //so you have to add a new host and create a new struct for them.
                effectController.InitHostControllerStacking(host, stacksApplied);
                
            } //end if the effect was created previously
           
            InitEffect(host);

        } //endCreateEffectSingle (2)


        public virtual void CreateStackingEffectMultiple(List<BattleObject> list, int stacksApplied) {

            effectManager = GameObject.Find("EffectManager");
            EffectController effectController;

            if (CheckForExistingEffectController(effectManager, this) == null) {
                effectController = effectManager.AddComponent<EffectController>();
                effectController.effectApplied = this;
                effectManager.GetComponent<EffectManager>().activeEffectControllerList.Add(effectController);
                foreach (BattleObject newHost in list) {
                    effectController.InitHostControllerStacking(newHost, stacksApplied);
                }
                return;
            }
            else {
                effectController = CheckForExistingEffectController(effectManager, this);
            }

            List<BattleObject> listToInit = new List<BattleObject>(list);

            foreach (BattleObject existingHost in list) {

                foreach (EffectController.HostController hostController in effectController.hostControllerList) {
                    if (hostController.host == existingHost) {
                        effectController.UpdateStacks(hostController, stacksApplied);
                        listToInit.Remove(existingHost);
                    }
                } //Now you've updated any existing hosts, and removed them from the list to pass if so
            } //end foreach host in list

            foreach (BattleObject newHost in listToInit) {
                effectController.InitHostControllerStacking(newHost, stacksApplied);
                InitEffect(newHost);
            }
            
        } //end CreateStackingEffectMultiple(2)

        
        public EffectController CheckForExistingEffectController (GameObject effectManager, Effect effectToCheckFor) {
            foreach (EffectController effectController in effectManager.GetComponent<EffectManager>().activeEffectControllerList) {
                if (effectController.effectApplied == this) {
                    return effectController;
                } 
            }
            return null;
        } //end CheckForExistingEffectController(2)
        
        
    } //end Effect Class

} //end Effects namespace