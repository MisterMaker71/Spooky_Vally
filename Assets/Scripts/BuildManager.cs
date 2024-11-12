using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildingGrid[] grids = new BuildingGrid[0];
    public static bool isGridSelected = false;
    public static int selectedGrid = 0;
    private void Start()
    {
        grids = FindObjectsOfType<BuildingGrid>();
    }
    void Update()
    {
        isGridSelected = false;
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].isSelected)
            {
                selectedGrid = i;
                isGridSelected = true;
            }
        }
    }
}
