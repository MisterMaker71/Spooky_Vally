using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform playerBoddy;
    public Transform outline;
    public LayerMask interactionLayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10, interactionLayer))
        {
            if (Vector3.Distance(new Vector3(hit.point.x, 0, hit.point.z), playerBoddy.position) < 3.5f)
            {
                outline.gameObject.SetActive(true);
                outline.position = new Vector3(Mathf.Floor(hit.point.x) + 0.5f, 0, Mathf.Floor(hit.point.z) + 0.5f);
                //Debug.DrawRay(new Vector3(Mathf.Floor(hit.point.x) + 0.5f, 0, Mathf.Floor(hit.point.z) + 0.5f), Vector3.up, Color.red, 1);
            }
            else
                outline.gameObject.SetActive(false);
        }
        else
            outline.gameObject.SetActive(false);
    }
}
