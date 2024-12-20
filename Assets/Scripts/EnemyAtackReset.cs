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

            if (Vector3.Distance(transform.position, PlayerMovement.PlayerInstance.transform.position) <= g.attackRange + 0.2f)
            {
                PlayerMovement.PlayerInstance.takedmg(g.attackDmg);
                Debug.DrawLine(transform.position, transform.position + Vector3.right * (g.attackRange + 0.2f), Color.red, 2);
                Debug.DrawLine(transform.position, transform.position + Vector3.left * (g.attackRange + 0.2f), Color.red, 2);
                Debug.DrawLine(transform.position, transform.position + Vector3.forward * (g.attackRange + 0.2f), Color.red, 2);
                Debug.DrawLine(transform.position, transform.position + Vector3.back * (g.attackRange + 0.2f), Color.red, 2);
            }
            g.isAtacking = false;
        }
        else
        {
            Debug.LogWarning("missing Enemy");
            PlayerMovement.PlayerInstance.takedmg(5);
        }
    }
    public void Atack()
    {
        if (g != null)
        {
            if (g.animator != null)
                g.animator.SetInteger("schlagen", 0);

            if (Vector3.Distance(transform.position, PlayerMovement.PlayerInstance.transform.position) <= g.attackRange + 0.2f)
            {
                PlayerMovement.PlayerInstance.takedmg(g.attackDmg);
                Debug.DrawLine(transform.position, transform.position + Vector3.right * (g.attackRange + 0.2f), Color.red, 2);
                Debug.DrawLine(transform.position, transform.position + Vector3.left * (g.attackRange + 0.2f), Color.red, 2);
                Debug.DrawLine(transform.position, transform.position + Vector3.forward * (g.attackRange + 0.2f), Color.red, 2);
                Debug.DrawLine(transform.position, transform.position + Vector3.back * (g.attackRange + 0.2f), Color.red, 2);
            }
        }
        else
        {
            Debug.LogWarning("missing Enemy");
            PlayerMovement.PlayerInstance.takedmg(5);
        }
    }
    public void End()
    {
        if (g != null)
        {
            g.isAtacking = false;
        }
        else
        {
            Debug.LogWarning("missing Enemy");
        }
    }
}
