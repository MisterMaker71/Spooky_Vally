using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimedEvent { NoEvent, BlackholeSun, BlodMoon,  }
public class TimedEvents : MonoBehaviour
{
    public static TimedEvent timedEvent;
    public static TimedEvent nextEvent;
    public DayNightCical NoEvent;
    public DayNightCical BlackholeSun;
    public DayNightCical BlodMoon;
    private void Start()
    {
        NoEvent.changeDaytime.AddListener(ChangeEvent);
        BlackholeSun.changeDaytime.AddListener(ChangeEvent);
        BlodMoon.changeDaytime.AddListener(ChangeEvent);
    }
    void Update()
    {
        
    }
    public void SetEvent(TimedEvent te)
    {
        //float time = DayNightCical.timeOfDay;
        NoEvent.gameObject.SetActive(false);
        BlackholeSun.gameObject.SetActive(false);
        BlodMoon.gameObject.SetActive(false);
        switch (te)
        {
            case TimedEvent.NoEvent:
                NoEvent.gameObject.SetActive(true);
                //RenderSettings.sun = NoEvent.GetComponent<Light>();
                RenderSettings.fog = false;
                break;
            case TimedEvent.BlackholeSun:
                BlackholeSun.gameObject.SetActive(true);
                //RenderSettings.sun = BlackholeSun.GetComponent<Light>();
                RenderSettings.fog = true;
                RenderSettings.fogColor = new Color(64 / 50, 49 / 50, 48 / 50);
                break;
            case TimedEvent.BlodMoon:
                BlodMoon.gameObject.SetActive(true);
                //RenderSettings.sun = BlodMoon.GetComponent<Light>();
                RenderSettings.fog = true;
                RenderSettings.fogColor = new Color(92 / 50, 8 / 50, 3 / 50);
                break;
        }
    }
    public void ChangeEvent(DayTime time)
    {
        int random = Random.Range(50, 100 + 1);
        if (random >= 0 && 69 >= random) ChangeEvent(TimedEvent.NoEvent);
        if (random >= 70 && 74 >= random) ChangeEvent(TimedEvent.BlackholeSun);
        if (random >= 85 && 100 >= random) ChangeEvent(TimedEvent.BlodMoon);

        switch (nextEvent)
        {
            case TimedEvent.NoEvent:
                SetEvent(TimedEvent.NoEvent);
                break;
            case TimedEvent.BlackholeSun:
                if (time == DayTime.Day)
                    SetEvent(TimedEvent.BlackholeSun);
                else
                    SetEvent(TimedEvent.NoEvent);
                break;
            case TimedEvent.BlodMoon:
                if (time == DayTime.Night)
                    SetEvent(TimedEvent.BlodMoon);
                else
                    SetEvent(TimedEvent.NoEvent);
                break;
        }
    }
    public void ChangeEvent(TimedEvent te)
    {
        nextEvent = te;
    }
}
