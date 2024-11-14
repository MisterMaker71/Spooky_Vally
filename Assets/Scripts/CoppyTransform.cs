using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoppyTransform : MonoBehaviour
{
    public Transform target;
    public Vector3 PositionOffset;
    public Vector3 RotationOffset;
    public bool CoppyPosition = true;
    public bool CoppyRotateX = true;
    public bool CoppyRotateY = true;
    public bool CoppyRotateZ = true;
    void Update()
    {
        if(target != null)
        {
            if(CoppyPosition)
            {
                transform.position = target.position;
                transform.position += target.up * PositionOffset.y + target.right * PositionOffset.x + target.forward * PositionOffset.z;
            }
            //transform.rotation = target.rotation;
            if (CoppyRotateX)
                transform.localEulerAngles = new Vector3(target.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
            if (CoppyRotateY)
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, target.localEulerAngles.y, transform.localEulerAngles.z);
            if (CoppyRotateZ)
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, target.localEulerAngles.z);
            transform.Rotate(RotationOffset);
        }
    }
}
