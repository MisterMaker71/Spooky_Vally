using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public AudioMixer Mixer;

    public Slider MasterVlolume;
    public TMP_Text MasterDisplay;

    public Slider AmbianceVlolume;
    public TMP_Text AmbianceDisplay;

    public Slider EffectVlolume;
    public TMP_Text EffectDisplay;

    public Slider OtherVlolume;
    public TMP_Text OtherDisplay;
    private void Start()
    {
        MasterVlolume.value = PlayerPrefs.GetFloat("MasterVolume", 0);
        AmbianceVlolume.value = PlayerPrefs.GetFloat("AmbianceVolume", 0);
        EffectVlolume.value = PlayerPrefs.GetFloat("EffectVolume", 0);
        OtherVlolume.value = PlayerPrefs.GetFloat("OtherVolume", 0);
    }
    void Update()
    {
        Mixer.SetFloat("MasterVolume", MasterVlolume.value);
        if (MasterVlolume.value > 0)
            MasterDisplay.text = "+" + MasterVlolume.value;
        else
            MasterDisplay.text = MasterVlolume.value.ToString();
        PlayerPrefs.SetFloat("MasterVolume", MasterVlolume.value);


        Mixer.SetFloat("AmbianceVolume", AmbianceVlolume.value);
        if (AmbianceVlolume.value > 0)
            AmbianceDisplay.text = "+" + AmbianceVlolume.value;
        else
            AmbianceDisplay.text = AmbianceVlolume.value.ToString();
        PlayerPrefs.SetFloat("AmbianceVolume", AmbianceVlolume.value);


        Mixer.SetFloat("EffectVolume", EffectVlolume.value);
        if (EffectVlolume.value > 0)
            EffectDisplay.text = "+" + EffectVlolume.value;
        else
            EffectDisplay.text = EffectVlolume.value.ToString();
        PlayerPrefs.SetFloat("EffectVolume", EffectVlolume.value);


        Mixer.SetFloat("OtherVolume", OtherVlolume.value);
        if (OtherVlolume.value > 0)
            OtherDisplay.text = "+" + OtherVlolume.value;
        else
            OtherDisplay.text = OtherVlolume.value.ToString();
        PlayerPrefs.SetFloat("OtherVolume", OtherVlolume.value);
    }
    public void SetEnabeled(bool state)
    {
        MasterVlolume.gameObject.SetActive(state);
        AmbianceVlolume.gameObject.SetActive(state);
        EffectVlolume.gameObject.SetActive(state);
        OtherVlolume.gameObject.SetActive(state);
    }
    public void ToggleEnabeled()
    {

        MasterVlolume.gameObject.SetActive(!MasterVlolume.gameObject.activeSelf);
        AmbianceVlolume.gameObject.SetActive(!AmbianceVlolume.gameObject.activeSelf);
        EffectVlolume.gameObject.SetActive(!EffectVlolume.gameObject.activeSelf);
        OtherVlolume.gameObject.SetActive(!OtherVlolume.gameObject.activeSelf);
    }
}
