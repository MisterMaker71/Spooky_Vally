using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CammeraExtander : MonoBehaviour
{
    public float maxDistance = -4;
    public float moveSpeed = 23;
    [SerializeField] LayerMask cameraCollisionLayer;
    // Start is called before the first frame update
    void Start()
    {
        transform.position =transform.parent.position + transform.parent.forward * maxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.parent.position, transform.parent.forward * (maxDistance - -1) / (1 - -1), out hit, Mathf.Abs(maxDistance) + 0.4f, cameraCollisionLayer))
        {
            transform.position = Vector3.MoveTowards(transform.position, hit.point - (transform.parent.forward * ((maxDistance - -1) / (1 - -1) * 0.2f)), Time.deltaTime * moveSpeed);
            Debug.DrawLine(transform.parent.position, hit.point, Color.white);
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.position + transform.parent.forward * maxDistance, Time.deltaTime * moveSpeed);
        }
    }
}
