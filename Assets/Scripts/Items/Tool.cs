using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Tool : Item
{
    public int maxDurebility = 100;
    public int Durebility = 100;

    public UnityEvent use;
    public UnityEvent heal;
    public void Heal()
    {
        Heal(0);
    }
    public void Heal(int durebility)
    {
        heal.Invoke();
        Durebility += durebility;
        if (Durebility > maxDurebility)
            Durebility = maxDurebility;
    }
    public bool Use()
    {
        return Use(0);
    }
    public bool Use(int durebility)
    {
        use.Invoke();
        Durebility -= durebility;
        if (Durebility <= 0)
            return false;
        return true;
    }
}
