using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class PrefabPlacer : EditorWindow
{
    [MenuItem("Tools/Qualety Tools")]
    public static void ShowExample()
    {
        PrefabPlacer wnd = GetWindow<PrefabPlacer>();
        wnd.titleContent = new GUIContent("Qualety Tools");
    }
    private void OnGUI()
    {
        //Event e = Event.current;
        //if (e.type == EventType.KeyDown)
        //{
        //    if (e.keyCode == KeyCode.B)
        //    {
        //        Debug.Log("ye");
        //    }
        //}
        if(Selection.transforms.Length > 0)
        {
            if (GUILayout.Button("Rotate Y"))
            {
                Undo.RecordObjects(Selection.activeTransform.GetComponentsInChildren<Transform>(), "rotated on y");
                foreach (Transform item in Selection.transforms)
                {
                    item.rotation = Quaternion.Euler(item.rotation.x, Random.Range(-180, 181), item.rotation.z);
                }
            }
            if (GUILayout.Button("Rotate Y Children"))
            {
                Undo.RecordObjects(Selection.activeTransform.GetComponentsInChildren<Transform>(), "rotated children on y");
                for (int i = 0; i < Selection.activeTransform.childCount; i++)
                {
                    Selection.activeTransform.GetChild(i).rotation = Quaternion.Euler(Selection.activeTransform.GetChild(i).rotation.x, Random.Range(-180, 181), Selection.activeTransform.GetChild(i).rotation.z);
                }
            }
            if (GUILayout.Button("Useless button"))
            {
                //Undo.RecordObjects(null, "rotated children on y");
            }
        }
    }
}
