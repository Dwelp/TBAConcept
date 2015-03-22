using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TestInspector))]
public class TestInspectorEditor : Editor {

    public override void OnInspectorGUI()
    {
        TestInspector myTarget = (TestInspector)target;

        myTarget.obj = (UnitAction)EditorGUILayout.ObjectField("Object", myTarget.obj, typeof(UnitAction), true);

        if (myTarget.obj != null)
        {
            Editor tempEditor;
            tempEditor = Editor.CreateEditor(myTarget.obj);
            tempEditor.OnInspectorGUI();
        }

    }
}
