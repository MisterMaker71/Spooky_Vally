using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PointsDisplay : MonoBehaviour
{
    public TMP_Text points;
    void Update()
    {
        if (points != null)
            points.text = "Points: " + PlayerPrefs.GetInt("Points", 0);
    }
}
