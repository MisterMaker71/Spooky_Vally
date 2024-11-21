using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackReset : MonoBehaviour
{
    public void ResetSword()
    {
        PlayerMovement.PlayerInstance.schlagType = 0;
    }
}
