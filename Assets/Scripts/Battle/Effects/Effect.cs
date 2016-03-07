using UnityEngine;
using UnityEngine.UI;
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
        
        public float resolveScale {
            get; set;
        }

        public BattleObject effectedBattleObject {
            get; set;
        }

		public GameObject effectDisplayPrefab;

        public Effect () {

            effectDuration = 0;
            canStack = false;
            stackCount = 0;
            resolveScale = 1;

        } //end constructor
        

		//Effect display object stuff

		public TextMesh displayTextMesh;

	
		//Virtual functions, to be overridden on child classes as needed

		//I feel like most effects won't need a map.
		//But, for those that decay over time or do anything special,
		//orrr are triggered by events, I think they'll need that?
		//InvokeRepeating won't work for something that needs to check every frame.
		//Might need to research if there's a way to suppress update or something,
		//because if there is an effect script on a hero's ability like there needs to be,
		//it'll just get called all day.

        public virtual void EffectMap(BattleObject host) {
			
        }


        public virtual void InitEffect(BattleObject host) {
            
			effectedBattleObject = host;
			StartCoroutine (CheckForExpiration ());
			effectedBattleObject.effectList.Add(this);
			SpawnDisplayObject();
            //Spawn prefab as child of enemy (panel eventually)

        }

        public virtual void RemoveEffect() {
			effectedBattleObject.effectList.Remove (this);
			Destroy (effectDisplayPrefab);
        }

		public virtual IEnumerator CheckForExpiration() {
			yield return new WaitForSeconds (effectDuration);
			RemoveEffect ();
		}



/*        public virtual void CheckForExpiration() {
            if ((effectStartTimer + effectDuration) <= Time.time) {
                RemoveEffect();
            }
        }
*/

		public virtual void SpawnDisplayObject() {
			Vector3 spawnPosition = effectedBattleObject.transform.position;
			effectDisplayPrefab = (GameObject)Instantiate (Resources.Load ("EffectPrefab"),
				                                 spawnPosition,
				                                 Quaternion.Euler (90, 0, 0)
			                                 );
			displayTextMesh = effectDisplayPrefab.GetComponentInChildren<TextMesh> ();
			displayTextMesh.text = effectDisplayText;

            //effectDisplayPrefab.AddComponent <EffectClass>();

		} //end SpawnDisplayObject()



    } //end Effect Class

} //end Effects namespace
