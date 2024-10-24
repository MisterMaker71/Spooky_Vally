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

    }
}
