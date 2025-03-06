using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Tool : Item
{
    Slider damageSlider = null;
    public int maxDurebility = 100;
    public int Durebility = 100;
    public int DurebilityLoss = 10;

    public int animationType = 0;

    /// <summary>
    /// Fired when using Mouse(0)
    /// </summary>
    public UnityEvent use;
    /// <summary>
    /// Fired when using Mouse(1)
    /// </summary>
    public UnityEvent useS;
    /// <summary>
    /// Fired when adding durebility
    /// </summary>
    public UnityEvent heal;
    void Start()
    {
        Init();
        ToolInit();
    }
    /// <summary>
    /// Creates Durability meter
    /// </summary>
    public void ToolInit()
    {
        if (CreateItemCount)
        {
            damageSlider = GetComponentInChildren<Slider>();
            if (damageSlider == null)
            {
                GameObject g = Instantiate(Resources.Load<GameObject>("ItemDamage"), transform);
                if (g != null)
                    damageSlider = g.GetComponent<Slider>();
                if(damageSlider != null)
                {
                    damageSlider.maxValue = maxDurebility;
                    damageSlider.value = Durebility;
                    damageSlider.gameObject.SetActive(Durebility < maxDurebility);
                }
            }
        }
    }
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
        damageSlider.gameObject.SetActive(Durebility < maxDurebility);
    }
    public void Use()
    {
        //PlayerMovement.PlayerInstance.schlagType = animationType;
        if (!Use(DurebilityLoss))
        {
            count -= 1;
            if (count <= 0)
            {
                InventoryManager.MainInstance.ChangeSlot();
                Destroy(gameObject);
            }
            else
            {
                Durebility += maxDurebility;
                damageSlider.gameObject.SetActive(Durebility < maxDurebility);
            }
        }
    }
    public bool Use(int durebility)
    {
        use.Invoke();
        Durebility -= durebility;
        if (damageSlider != null)
        {
            damageSlider.value = Durebility;
            damageSlider.gameObject.SetActive(Durebility < maxDurebility);
        }
        if (Durebility <= 0)
            return false;
        return true;
    }
    public void UseSec()
    {
        //PlayerMovement.PlayerInstance.schlagType = animationType;
        if (!UseSec(DurebilityLoss))
        {
            count -= 1;
            if (count <= 0)
            {
                InventoryManager.MainInstance.ChangeSlot();
                Destroy(gameObject);
            }
            else
            {
                Durebility += maxDurebility;
                damageSlider.gameObject.SetActive(Durebility < maxDurebility);
            }
        }
    }
    public bool UseSec(int durebility)
    {
        useS.Invoke();
        Durebility -= durebility;
        if (damageSlider != null)
        {
            damageSlider.value = Durebility;
            damageSlider.gameObject.SetActive(Durebility < maxDurebility);
        }
        if (Durebility <= 0)
            return false;
        return true;
    }
}
