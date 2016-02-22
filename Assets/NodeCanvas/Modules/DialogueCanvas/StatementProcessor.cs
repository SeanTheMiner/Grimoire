using UnityEngine;
using System.Collections;
using System;
using ParadoxNotion.Services;

namespace NodeCanvas.DialogueTrees{

	public static class StatementProcessor{

		///An *optional* coroutine to process a statement writing text over time to the 'speech' property of the actor as well as playing audio.
		///These things are though usualy handled by the GUI.
		public static void ProcessStatement(IStatement statement, IDialogueActor actor, Action finishCallback){
			MonoManager.current.StartCoroutine( Internal_ProcessStatement(statement, actor, finishCallback) );
		}
		
		static IEnumerator Internal_ProcessStatement(IStatement statement, IDialogueActor actor, Action finishCallback){

			actor.speech = null;
			if (statement.audio){

				AudioSource audioSource = null;
				if (actor is Component){
					audioSource = (actor as Component).gameObject.GetComponent<AudioSource>();
					if (audioSource == null){
						audioSource = (actor as Component).gameObject.AddComponent<AudioSource>();
					}
				} else {
					audioSource = MonoManager.current.gameObject.GetComponent<AudioSource>();
					if (audioSource == null){
						audioSource = MonoManager.current.gameObject.AddComponent<AudioSource>();
					}
				}

				audioSource.clip = statement.audio;
				audioSource.Play();

				float timer = 0;
				actor.speech = statement.text;
				while(timer < statement.audio.length){
					timer += Time.deltaTime;
					yield return null;
				}

				audioSource.clip = null;

			} else {

				for (var i= 0; i < statement.text.Length; i++){
					actor.speech += statement.text[i];
					yield return new WaitForSeconds(0.05f);
					var c = statement.text[i];
					if (c == '.' || c == '!' || c == '?')
						yield return new WaitForSeconds(0.5f);
					if (c == ',')
						yield return new WaitForSeconds(0.1f);
				}

				yield return new WaitForSeconds(1.2f);
			}

			actor.speech = null;
			finishCallback();
		}
	}
}