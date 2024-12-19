using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Tool : Item
{
    public int maxDurebility = 100;
    public int Durebility = 100;
    public int DurebilityLoss = 10;

    public int animationType = 0;

    public UnityEvent use;
    public UnityEvent useS;
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
    public void Use()
    {
        //PlayerMovement.PlayerInstance.schlagType = animationType;
        if (!Use(DurebilityLoss))
        {
            if (count <= 0)
            {
                InventoryManager.MainInstance.ChangeSlot();
                Destroy(gameObject);
            }
            else
            {
                count -= 1;
                Durebility += maxDurebility;
            }
        }
    }
    public bool Use(int durebility)
    {
        use.Invoke();
        Durebility -= durebility;
        if (Durebility <= 0)
            return false;
        return true;
    }
    public void UseSec()
    {
        //PlayerMovement.PlayerInstance.schlagType = animationType;
        if (!UseSec(DurebilityLoss))
        {
            if (count <= 0)
            {
                InventoryManager.MainInstance.ChangeSlot();
                Destroy(gameObject);
            }
            else
            {
                count -= 1;
                Durebility += maxDurebility;
            }
        }
    }
    public bool UseSec(int durebility)
    {
        useS.Invoke();
        Durebility -= durebility;
        if (Durebility <= 0)
            return false;
        return true;
    }
}
