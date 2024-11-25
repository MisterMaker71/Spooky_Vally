using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class ItemInfo : MonoBehaviour
{
    public string Title = "Item Name";
    [Multiline(10)]
    public string Description = "This is a Item!";
    [Space(10)]
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;
    void Update()
    {
        if (description != null)
            description.text = Description;
        if (title != null)
            title.text = Title;
    }
}
