using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : Sword
{
    // Start is called before the first frame update

    private void Start()
    {
        Init();
        useS.AddListener(RemoveBuilding);
        use.AddListener(Atack);
    }
    public void RemoveBuilding()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10) && Time.timeScale == 1)
        {
            Buildebel b = hit.transform.GetComponentInParent<Buildebel>() as Buildebel;
            if (b != null)
            {
                if(b.canBeRemoved)
                {
                    foreach (string item in b.itemNames)
                    {
                        InventoryManager.MainInstance.AddItem(item, 1);
                    }
                    Destroy(b.gameObject);
                }
            }
        }
    }
}
