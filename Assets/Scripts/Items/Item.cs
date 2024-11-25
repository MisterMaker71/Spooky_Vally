using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Item : MonoBehaviour
{
    TMP_Text countText = null;
    public string Name = "Item Name";
    [Multiline(5)]
    public string Description = "This is a Item!";
    public int count = 1;
    // Start is called before the first frame update
    void Start()
    {
        GameObject g = Instantiate(Resources.Load<GameObject>("ItemCount"), transform);
        if(g != null)
            countText = g.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countText != null)
        {
            if (count > 1)
            {
                countText.text = count.ToString();
                countText.gameObject.SetActive(true);
            }
            else
            {
                countText.gameObject.SetActive(false);
            }
        }
    }
}
