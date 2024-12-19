using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CammeraExtander : MonoBehaviour
{
    public bool extendet = true;
    public float maxDistance = -4;
    float mD = 0;
    public float moveSpeed = 23;
    [SerializeField] LayerMask cameraCollisionLayer;
    //[HideInInspector]
    float distance = 0;
    public float Distance { get { return distance; } }
    // Start is called before the first frame update
    void Start()
    {
        mD = maxDistance;
        transform.position = transform.parent.position + transform.parent.forward * mD;
    }

    // Update is called once per frame
    void Update()
    {
        if(extendet)
        {
            if (maxDistance > 0)
            {
                if (mD < maxDistance)
                    mD += Time.deltaTime * moveSpeed;
                else
                    mD = maxDistance;
            }
            else
            {
                if (mD > maxDistance)
                    mD -= Time.deltaTime * moveSpeed;
                else
                    mD = maxDistance;
            }
        }
        else
        {
            if(maxDistance > 0)
            {
                if (mD > 0)
                    mD -= Time.deltaTime * moveSpeed;
                else
                    mD = 0;
            }
            else
            {
                if (mD < 0)
                    mD += Time.deltaTime * moveSpeed;
                else
                    mD = 0;
            }
        }

        RaycastHit hit;
        if(Physics.Raycast(transform.parent.position, transform.parent.forward * (mD - -1) / (1 - -1), out hit, Mathf.Abs(mD) + 0.2f, cameraCollisionLayer))
        {
            if (hit.distance <= 0.5f && mD >= 0.1f)
                transform.position = Vector3.MoveTowards(transform.position, hit.point - ((transform.parent.forward * hit.distance) * ((mD - -1) / (1 - -1))), Time.deltaTime * moveSpeed);
            else
                transform.position = Vector3.MoveTowards(transform.position, hit.point - ((transform.parent.forward * 0.2f) * ((mD - -1) / (1 - -1))) , Time.deltaTime * moveSpeed);
            Debug.DrawLine(transform.parent.position, hit.point, Color.white);
            Debug.DrawLine(transform.position, hit.point, Color.red);
            //distance = Vector3.Distance(transform.position, transform.parent.position);
        }
        else
        {
            //distance = Vector3.Distance(transform.position, transform.parent.position);
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.position + transform.parent.forward * mD, Time.deltaTime * moveSpeed);
        }
        distance = Vector3.Distance(transform.position, transform.parent.position);
    }
}
