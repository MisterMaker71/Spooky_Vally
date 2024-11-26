using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBuildingGrid : MonoBehaviour
{
    public LayerMask Mask;
    public Material m;
    public MeshRenderer gridRenderer;
    float f = 0;
    public float speed = 5;
    Material mat;
    private void Start()
    {
        mat = Instantiate(m);
        gridRenderer.material = mat;
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 15, Mask))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.point);
            mat.SetVector("_Position", hit.point);
            if (f > 0)
                f -= Time.deltaTime * speed;
            else f = 0;
        }
        else
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(r.origin, r.direction);
            if (f < 1)
                f += Time.deltaTime * speed;
            else f = 1;
        }
        mat.SetFloat("_camera_dependent", f);
    }
}