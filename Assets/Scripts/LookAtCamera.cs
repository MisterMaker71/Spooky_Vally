using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LookMode { ZUp, BilBord, LookAt }
public class LookAtCamera : MonoBehaviour
{
    [SerializeField] LookMode lookMode;
    private void LateUpdate()
    {
        switch (lookMode)
        {
            case LookMode.ZUp:
                transform.rotation = Quaternion.Euler(new Vector3(0, Camera.main.transform.eulerAngles.y, 0));
                break;
            case LookMode.BilBord:
                transform.eulerAngles = Camera.main.transform.eulerAngles;
                break;
            case LookMode.LookAt:
                transform.LookAt(Camera.main.transform.position);
                transform.Rotate(Vector3.up * 180);
                break;
        }
    }
}
