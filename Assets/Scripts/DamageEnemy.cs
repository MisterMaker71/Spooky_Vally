using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    /// <summary>
    /// damege of next atack
    /// </summary>
    public static float damage = 10;
    public void Damage()
    {
        foreach (Gegner g in FindObjectsOfType<Gegner>())
        {
            if (Vector3.Distance(PlayerMovement.PlayerInstance.damagePosition.position, g.transform.position) < 2.5f)
            {
                g.takedmg(damage);
            }
        }
    }
}
