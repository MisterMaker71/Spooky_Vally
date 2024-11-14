using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class PrefabPlacer : EditorWindow
{
    bool children = false;
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
        //maxs = maxs.Trim(c);
        //mins = mins.Trim(c);
        bool b = Selection.transforms.Length > 0;
        if(children)
        {
            b = Selection.activeTransform.childCount > 0;
        }
        if (b)
        {
            children = GUILayout.Toggle(children, "Use children transform");
            if (GUILayout.Button("Rotate Y"))
            {
                if(!children)
                {
                    Undo.RecordObjects(Selection.transforms, "rotated on y");
                    foreach (Transform item in Selection.transforms)
                    {
                        item.rotation = Quaternion.Euler(item.rotation.x, Random.Range(-180, 181), item.rotation.z);
                    }
                }
                else
                {
                    Undo.RecordObjects(Selection.activeTransform.GetComponentsInChildren<Transform>(), "rotated children on y");
                    for (int i = 0; i < Selection.activeTransform.childCount; i++)
                    {
                        Selection.activeTransform.GetChild(i).rotation = Quaternion.Euler(Selection.activeTransform.GetChild(i).rotation.x, Random.Range(-180, 181), Selection.activeTransform.GetChild(i).rotation.z);
                    }
                }
            }
            if (GUILayout.Button("Random Scale (0.9 - 1.2)"))
            {
                if (!children)
                {
                    Undo.RecordObjects(Selection.transforms, "randomised scale");
                    foreach (Transform item in Selection.transforms)
                    {
                        item.localScale = Vector3.one * Random.Range(0.9f, 1.2f);
                    }
                }
                else
                {
                    Undo.RecordObjects(Selection.activeTransform.GetComponentsInChildren<Transform>(), "randomised scale of children");
                    for (int i = 0; i < Selection.activeTransform.childCount; i++)
                    {
                        Selection.activeTransform.GetChild(i).localScale = Vector3.one * Random.Range(0.9f, 1.2f);
                    }
                }
            }
        }
        else
        {
            children = GUILayout.Toggle(children, "Use children transform");
            GUI.color = Color.gray;
            GUILayout.Button("Rotate Y");
            GUILayout.Button("Random Scale (0.9 - 1.2)");
        }
        //string removeText(string s)
        //{
        //    string str = "";
        //    foreach (char item in s)
        //    {
        //        foreach (char ch in c)
        //        {
        //            if (item == ch)
        //                str += item;
        //        }
        //    }
        //    return str;
        //}
        //double ToDouble(string s)
        //{
        //    if (s == "")
        //        return 0;
        //    try
        //    {
        //        string str = "";
        //        foreach (char item in s)
        //        {
        //            foreach (char ch in c)
        //            {
        //                if (item == ch)
        //                    str += s;
        //            }
        //        }
        //        Debug.Log(str);
        //        double f = System.Convert.ToDouble(str);
        //        return f;
        //    }
        //    catch (System.Exception)
        //    {
        //        return 0.5f;
        //    }
        //}
    }
    private void OnSelectionChange()
    {
        //Refresh
        //GUILayout.
    }
}
