/*


using UnityEngine;
using UnityEditor;
using System.Collections;

public class AbilityEditor : EditorWindow {

    string inTextField = "Set text field";
    bool groupEnabled;

    public Rect windowRect = new Rect(100, 100, 200, 200);
    
    void OnGui() {

        BeginWindows();

        windowRect = GUILayout.Window(1, windowRect, DoWindow, "Hi There");
        
        //GUILayout.Label("Ability Editor", EditorStyles.boldLabel);
        //inTextField = EditorGUILayout.TextField("Text Field", inTextField);
        
        EndWindows();
    }
    
    void DoWindow (int unusedWindowID) {
        GUILayout.Button("Button");
        GUI.DragWindow();
    }


    [MenuItem("Window/AbilityEditor")]
    static void Init() {
        EditorWindow.GetWindow(typeof(AbilityEditor));
    }


} //end AbilityEditor class



*/