using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public void CoppyHumanoid(Transform characterRoot)
    {
        CoppyHumanoid(transform, characterRoot);
    }
    public void CoppyHumanoid(Transform root, Transform characterRoot)
    {
        //print("ragdolling");
        Transform[] parts = GetComponentsInChildren<Transform>();
        foreach (Transform part in parts)
        {
            //print(part.name);
            Transform t = FindSame(characterRoot, part.name);
            if(t != null)
            {
                Rigidbody rb = part.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
                part.position = t.position;
                part.rotation = t.rotation;
            }
        }
    }
    public Transform FindSame(Transform charRoot, string Name)
    {

        foreach (Transform item in charRoot.GetComponentsInChildren<Transform>())
        {
            if (Name == item.name)
            {
                return item;
            }
        }
        return null;
    }
}
