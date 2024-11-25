using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partical
{
    /// <summary>
    /// spawns a partical effect
    /// </summary>
    /// <param name="name">name of partical</param>
    public static void Create(string name, Vector3 position)
    {
        GameObject p = Resources.Load<GameObject>("Particals/" + name);
        if (p != null)
        {
            GameObject g = GameObject.Instantiate(p, position, p.transform.rotation);
            //g.transform.right = direction;
        }
    }
}
