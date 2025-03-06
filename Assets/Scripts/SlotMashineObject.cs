using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMashineObject : MonoBehaviour
{
    public Transform wheal;
    float spinTime = -100;
    public Vector3 offset;
    public Vector3 direction;
    private void Update()
    {
        if (spinTime > 0)
        {
            if(spinTime > 5)
                wheal.Rotate(new Vector3(0, 5, 0));
            else
                wheal.Rotate(new Vector3(0, spinTime, 0));
            spinTime -= Time.deltaTime;
        }
        if (spinTime <= 0 && spinTime > -99)
        {
            spinTime = -100;
            RaycastHit hit;
            if (Physics.Raycast(wheal.position + (transform.right * offset.x + transform.up * offset.y + transform.forward * offset.z), (transform.right * direction.x + transform.up * direction.y + transform.forward * direction.z), out hit, 0.4f))
            {
                print("win = " + hit.transform.name);
                FindFirstObjectByType<SlotMashine>().spin(hit.transform.name == "WinParts");
            }
        }
    }
    public void openHud()
    {
        FindFirstObjectByType<SlotMashine>().Open(transform.position);
    }
    public void Spin()
    {
        print("Lets go Gambling");
        spinTime = Random.Range(5f, 8f);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(wheal.position + (transform.right * offset.x + transform.up * offset.y + transform.forward * offset.z), 0.01f);
        Gizmos.DrawLine(wheal.position + (transform.right * offset.x + transform.up * offset.y + transform.forward * offset.z), wheal.position + (transform.right * offset.x + transform.up * offset.y + transform.forward * offset.z) + ((transform.right * direction.x + transform.up * direction.y + transform.forward * direction.z).normalized / 10) * 0.4f);
    }
}
