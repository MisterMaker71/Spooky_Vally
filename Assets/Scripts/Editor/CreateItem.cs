using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateItem : EditorWindow
{
    public string Name;
    [MenuItem("Tools/Create Item")]
    public static void ShowExample()
    {
        CreateItem wnd = GetWindow<CreateItem>();
        wnd.titleContent = new GUIContent("Create Item");
    }
    public enum ItemType{ Item, Crop, Seed, Wappon }
    private void OnGUI()
    {
        Name = GUILayout.TextField(Name);
        Name = GUILayout.TextField(Name);
        Name = GUILayout.TextField(Name);
        if (GUILayout.Button("Create"))
        {
            Undo.RecordObjects(Selection.activeTransform.GetComponentsInChildren<Transform>(), "Created new Item");
        }
    }
}
