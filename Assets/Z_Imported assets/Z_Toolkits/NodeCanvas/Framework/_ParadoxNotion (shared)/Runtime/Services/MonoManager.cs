using System.Collections.Generic;
using System;
using UnityEngine;


namespace ParadoxNotion.Services {

    ///Singleton. Automatically added when needed, collectively calls methods that needs updating amongst other things relative to MonoBehaviours
    public class MonoManager : MonoBehaviour {

        public event Action onUpdate;
        public event Action onLateUpdate;
        public event Action onFixedUpdate;
        public event Action onGUI;


        private List<Action> onUpdateCalls      = new List<Action>();
        private List<Action> onLateUpdateCalls  = new List<Action>();
        private List<Action> onFixedUpdateCalls = new List<Action>();
        private List<Action> onGUICalls         = new List<Action>();


        private static bool isQuiting;
        private static MonoManager _current;
        public static MonoManager current {
            get
            {
                if ( _current == null && !isQuiting ) {
                    _current = FindObjectOfType<MonoManager>();
                    if ( _current == null )
                        _current = new GameObject("_MonoManager").AddComponent<MonoManager>();
                }
                return _current;
            }
        }


        ///Creates MonoManager singleton
        public static void Create() { _current = current; }



        public static void AddUpdateMethod(Action method) { current.onUpdateCalls.Add(method); }
        public static void RemoveUpdateMethod(Action method) { current.onUpdateCalls.Remove(method); }

        public static void AddLateUpdateMethod(Action method) { current.onLateUpdateCalls.Add(method); }
        public static void RemoveLateUpdateMethod(Action method) { current.onLateUpdateCalls.Remove(method); }

        public static void AddFixedUpdateMethod(Action method) { current.onFixedUpdateCalls.Add(method); }
        public static void RemoveFixedUpdateMethod(Action method) { current.onFixedUpdateCalls.Remove(method); }

        public static void AddOnGUIMethod(Action method) { current.onGUICalls.Add(method); }
        public static void RemoveOnGUIMethod(Action method) { current.onGUICalls.Remove(method); }



        void Awake() {
            if ( _current != null && _current != this ) {
                DestroyImmediate(this.gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            _current = this;
        }

        void OnApplicationQuit() { isQuiting = true; }


        void Update(){

            if (Time.timeScale <= 0){
                return;
            }

            for (var i = 0; i < onUpdateCalls.Count; i++){
                onUpdateCalls[i]();
            }
            if (onUpdate != null){ onUpdate(); }
        }

        void LateUpdate(){

            if (Time.timeScale <= 0){
                return;
            }

            for (var i = 0; i < onLateUpdateCalls.Count; i++){
                onLateUpdateCalls[i]();
            }
            if (onLateUpdate != null){ onLateUpdate(); }
        }

        void FixedUpdate(){

            if (Time.timeScale <= 0){
                return;
            }

            for (var i = 0; i < onFixedUpdateCalls.Count; i++){
                onFixedUpdateCalls[i]();
            }
            if (onFixedUpdate != null){ onFixedUpdate(); }
        }

        void OnGUI(){

            if (Time.timeScale <= 0){
                return;
            }

            for (var i = 0; i < onGUICalls.Count; i++){
                onGUICalls[i]();
            }
            if (onGUI != null){ onGUI(); }
        }
    }
}