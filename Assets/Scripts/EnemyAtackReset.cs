using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtackReset : MonoBehaviour
{
    [SerializeField] Gegner g = null;
    private void Start()
    {
        if (g == null)
        {
            g = GetComponentInParent<Gegner>();
        }
    }
    public void ResetAtck()
    {
        if (g != null)
        {
            if (g.animator != null)
                g.animator.SetInteger("schlagen", 0);

            if (Vector3.Distance(transform.position, PlayerMovement.PlayerInstance.transform.position) <= g.attackRange)
                PlayerMovement.PlayerInstance.takedmg(g.attackDmg);
            g.isAtacking = false;
        }
        else
        {
            Debug.LogWarning("missing Enemy");
            PlayerMovement.PlayerInstance.takedmg(5);
        }
    }
}
