using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class PrefabPlacer : EditorWindow
{
    string mins = "no";
    string maxs = "no";
    new char[] c = { '0' , '1', '2', '3', '4', '4', '5' , '6', '7', '8' , '9' , ','};
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
            GUILayout.TextField("X");
            if (GUILayout.Button("Random Scale"))
            {
                //Undo.RecordObjects(null, "rotated children on y");
            }
        }
        else
        {
            GUI.color = Color.gray;
            GUILayout.Button("Rotate Y");
            GUILayout.Button("Rotate Y Children");
            GUI.color = Color.white;
            mins = GUILayout.TextField(removeText(mins));
            maxs = GUILayout.TextField(removeText(maxs));
            //maxs = removeText(maxs);
            //mins = removeText(mins);
            GUI.color = Color.gray;
            GUILayout.Button("Random Scale");
        }
        string removeText(string s)
        {
            string str = "";
            foreach (char item in s)
            {
                foreach (char ch in c)
                {
                    if (item == ch)
                        str += item;
                }
            }
            return str;
        }
        double ToDouble(string s)
        {
            if (s == "")
                return 0;
            try
            {
                string str = "";
                foreach (char item in s)
                {
                    foreach (char ch in c)
                    {
                        if (item == ch)
                            str += s;
                    }
                }
                Debug.Log(str);
                double f = System.Convert.ToDouble(str);
                return f;
            }
            catch (System.Exception)
            {
                return 0.5f;
            }
        }
    }
    private void OnSelectionChange()
    {
        //Refresh
        //GUILayout.
    }
}
