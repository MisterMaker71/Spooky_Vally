using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSettings : MonoBehaviour
{
    public TMP_Text sensText;
    public Slider sensSlider;
    void Start()
    {
        sensSlider.value = PlayerPrefs.GetFloat("sens_settings", 500 / 50);
    }
    public void Change(float value)
    {
        sensText.text = value.ToString();
        PlayerMovement.PlayerInstance.mouseSensebility = value * 50;
        PlayerPrefs.SetFloat("sens_settings", value);
    }
}
