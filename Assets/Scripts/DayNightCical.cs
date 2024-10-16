using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class DayNightCical : MonoBehaviour
{
    //[SerializeField] float y;
    //[SerializeField] float z;
    [Tooltip("Angel of the sun")]
    public float v = 0;
    Light sun;
    [Tooltip("Sun rotation (night is 5 times faster)")]
    [SerializeField] float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        sun = GetComponent<Light>();
        //y = transform.localRotation.eulerAngles.y;
        //z = transform.localRotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (v > 190 && v < 350)
            v += Time.deltaTime * speed * 5;
        else
            v += Time.deltaTime * speed;


        if (v > 360)
            v = 0;
        if (v < 0)
            v = 360;

        if (v > 90 && v < 270)
            transform.localRotation = Quaternion.Euler(new Vector3(v, 0, -0));
        else
            transform.localRotation = Quaternion.Euler(new Vector3(v, 0, 0));
        if (sun != null)
        {
            if (v > 10 && v < 160)
                sun.intensity += Time.deltaTime * speed / 15;
            if (v > 160 && v < 360)
                sun.intensity -= Time.deltaTime * speed / 15;
            sun.intensity = Mathf.Clamp(sun.intensity, 0, 1);
        }
    }
}
