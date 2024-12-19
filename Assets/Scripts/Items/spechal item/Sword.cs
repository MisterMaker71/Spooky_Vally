using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Wapon
{
    // Start is called before the first frame update

    private void Start()
    {
        Init();
        use.AddListener(Atack);
    }
    public void Atack()
    {
        if (PlayerMovement.PlayerInstance.schlagType == 0)
        {
            PlayerMovement.PlayerInstance.schlagType = animationType;
            Audioevent.playAudio("swing", transform.position);
            DamageEnemy.damage = damage;
        }
        //Inserte Damege Script here
        //print("sword used");
    }
}
