using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DayTime { Day, Night }
[RequireComponent(typeof(Light))]
public class DayNightCical : MonoBehaviour
{
    public static DayTime time;
    //[SerializeField] float y;
    //[SerializeField] float z;
    [Tooltip("Angel of the sun")]
    public static float timeOfDay = 0;
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
        if (timeOfDay > 190 && timeOfDay < 350)
        {
            time = DayTime.Night;
            timeOfDay += Time.deltaTime * speed * 5;
        }
        else
        {
            time = DayTime.Day;
            timeOfDay += Time.deltaTime * speed;
        }


        if (timeOfDay > 360)
            timeOfDay = 0;
        if (timeOfDay < 0)
            timeOfDay = 360;

        if (timeOfDay > 90 && timeOfDay < 270)
            transform.localRotation = Quaternion.Euler(new Vector3(timeOfDay, 0, -0));
        else
            transform.localRotation = Quaternion.Euler(new Vector3(timeOfDay, 0, 0));
        if (sun != null)
        {
            if(FarmManager.instance.Raining)
            {
                sun.intensity -= Time.deltaTime * speed / 5;
            }
            else
            {
                if (timeOfDay > 10 && timeOfDay < 160)
                    sun.intensity += Time.deltaTime * speed / 15;
                if (timeOfDay > 160 && timeOfDay < 360)
                    sun.intensity -= Time.deltaTime * speed / 15;
            }
            sun.intensity = Mathf.Clamp(sun.intensity, 0, 1);
        }
    }
}
