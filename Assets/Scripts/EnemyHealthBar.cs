using System.Collections;
using UnityEngine;using UnityEngine.UI;
[RequireComponent(typeof(Gegner))]
public class EnemyHealthBar : MonoBehaviour{
    public Image i;Gegner g;
    void Start(){g = GetComponent<Gegner>();}
    void Update(){if (i != null)i.fillAmount = g.health / g.maxHealth;}
}