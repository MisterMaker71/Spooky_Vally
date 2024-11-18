using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Wapon
{
    // Start is called before the first frame update

    private void Start()
    {
        use.AddListener(Atack);
    }
    public void Atack()
    {
        //Inserte Damege Script here
        //print("sword used");
        foreach(Gegner g in FindObjectsOfType<Gegner>())
        {
            if(Vector3.Distance(PlayerMovement.PlayerInstance.damagePosition.position, g.transform.position) < 2.5f)
            {
                g.takedmg(damage);
            }
        }
    }
}
