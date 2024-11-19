using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class TriggerEvent : MonoBehaviour
{
//    public Vector3 center = Vector3.zero;
//    public Vector3 size = Vector3.one;

    public UnityEvent Enter;
    public UnityEvent Stay;
    public UnityEvent Exit;
    BoxCollider collider;
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        if(collider == null)
        {
            Destroy(gameObject);
        }
        collider.isTrigger = true;
        //gameObject.layer = LayerMask.GetMask("Ignore Raycsst");
    }
    private void OnTriggerEnter(Collider other)
    {

        Enter.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {

        Exit.Invoke();
    }
    private void OnTriggerStay(Collider other)
    {

        Stay.Invoke();
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.green;

    //    Gizmos.DrawLine(transform.position + center + (transform.right * size.x / 2) + (transform.up * size.y / 2) + (transform.forward * size.z / 2), transform.position + center - (transform.right * size.x / 2) + (transform.up * size.y / 2) + (transform.forward * size.z / 2));
    //    Gizmos.DrawLine(transform.position + center + (transform.right * size.x / 2) - (transform.up * size.y / 2) + (transform.forward * size.z / 2), transform.position + center - (transform.right * size.x / 2) - (transform.up * size.y / 2) + (transform.forward * size.z / 2));
    //    Gizmos.DrawLine(transform.position + center + (transform.right * size.x / 2) + (transform.up * size.y / 2) - (transform.forward * size.z / 2), transform.position + center - (transform.right * size.x / 2) + (transform.up * size.y / 2) - (transform.forward * size.z / 2));
    //    Gizmos.DrawLine(transform.position + center + (transform.right * size.x / 2) - (transform.up * size.y / 2) - (transform.forward * size.z / 2), transform.position + center - (transform.right * size.x / 2) - (transform.up * size.y / 2) - (transform.forward * size.z / 2));

    //    Gizmos.DrawLine(transform.position + center + (transform.right * size.x / 2) + (transform.up * size.y / 2) + (transform.forward * size.z / 2), transform.position + center + (transform.right * size.x / 2) - (transform.up * size.y / 2) + (transform.forward * size.z / 2));
    //    Gizmos.DrawLine(transform.position + center - (transform.right * size.x / 2) + (transform.up * size.y / 2) + (transform.forward * size.z / 2), transform.position + center - (transform.right * size.x / 2) - (transform.up * size.y / 2) + (transform.forward * size.z / 2));
    //    Gizmos.DrawLine(transform.position + center + (transform.right * size.x / 2) + (transform.up * size.y / 2) - (transform.forward * size.z / 2), transform.position + center + (transform.right * size.x / 2) - (transform.up * size.y / 2) - (transform.forward * size.z / 2));
    //    Gizmos.DrawLine(transform.position + center - (transform.right * size.x / 2) + (transform.up * size.y / 2) - (transform.forward * size.z / 2), transform.position + center - (transform.right * size.x / 2) - (transform.up * size.y / 2) - (transform.forward * size.z / 2));

    //    Gizmos.DrawLine(transform.position + center + (transform.right * size.x / 2) + (transform.up * size.y / 2) + (transform.forward * size.z / 2), transform.position + center + (transform.right * size.x / 2) + (transform.up * size.y / 2) - (transform.forward * size.z / 2));
    //    Gizmos.DrawLine(transform.position + center - (transform.right * size.x / 2) + (transform.up * size.y / 2) + (transform.forward * size.z / 2), transform.position + center - (transform.right * size.x / 2) + (transform.up * size.y / 2) - (transform.forward * size.z / 2));
    //    Gizmos.DrawLine(transform.position + center + (transform.right * size.x / 2) - (transform.up * size.y / 2) + (transform.forward * size.z / 2), transform.position + center + (transform.right * size.x / 2) - (transform.up * size.y / 2) - (transform.forward * size.z / 2));
    //    Gizmos.DrawLine(transform.position + center - (transform.right * size.x / 2) - (transform.up * size.y / 2) + (transform.forward * size.z / 2), transform.position + center - (transform.right * size.x / 2) - (transform.up * size.y / 2) - (transform.forward * size.z / 2));
    //}
}
