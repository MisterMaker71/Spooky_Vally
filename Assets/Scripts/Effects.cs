using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effects
{
    [Header("Effect Stats")]
    [Header("use [-100] to disabel time")]
    [Space(5)]
    public int Speed = 0;
    public float SpeedTime = -100;
    [Space(10)]
    public int Luck = 0;
    public float LuckTime = -100;
    [Space(10)]
    public int Harvest = 0;
    public float HarvestTime = -100;
    [Space(10)]
    public int Blead = 0;
    public float BleadTime = -100;

    /// <summary>
    /// Useed to update Time.
    /// [used in parent Update methode]
    /// </summary>
    public void Update()
    {
        if(SpeedTime > 0) SpeedTime -= Time.deltaTime;
        else Speed = 0;

        if (LuckTime > 0) LuckTime -= Time.deltaTime;
        else Luck = 0;

        if (HarvestTime > 0) HarvestTime -= Time.deltaTime;
        else Harvest = 0;

        if (BleadTime > 0) BleadTime -= Time.deltaTime;
        else Blead = 0;

    }
    public void Add(string name, int level, float time)
    {
        if(GetLevel(name) < level)
            Set(name, level, GetTime(name) + time);
        else
            Set(name, GetLevel(name), GetTime(name) + time);
    }
    public void Remove(string name)
    {

    }
    public void Set(string name, int level, float time)
    {
        switch (name.ToLower())
        {
            case "speed":
                Speed = level;
                SpeedTime = time;
                break;

            case "luck":
                Luck = level;
                LuckTime = time;
                break;

            case "harvest":
                Harvest = level;
                HarvestTime = time;
                break;

            case "blead":
                Blead = level;
                BleadTime = time;
                break;

            default:
                Debug.LogWarning("Effect " + name + " dosen not exist.");
                break;
        }
    }
    public int GetLevel(string name)
    {
        switch (name.ToLower())
        {
            case "speed":
                return Speed;

            case "luck":
                return Luck;

            case "harvest":
                return Harvest;

            case "blead":
                return Blead;

            default:
                Debug.LogWarning("Effect " + name + " dosen not exist.");
                return 0;
        }
    }
    public float GetTime(string name)
    {
        switch (name.ToLower())
        {
            case "speed":
                return SpeedTime;

            case "luck":
                return LuckTime;

            case "harvest":
                return HarvestTime;

            case "blead":
                return BleadTime;

            default:
                Debug.LogWarning("Effect " + name + " dosen not exist.");
                return 0;
        }
    }
}
