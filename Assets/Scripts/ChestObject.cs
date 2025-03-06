using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestObject : PlacebelIStoragent
{
    public void Open()
    {
        if (FindFirstObjectByType<ChestManager>() != null)
            FindFirstObjectByType<ChestManager>().Open(transform.position, this);
    }
}
