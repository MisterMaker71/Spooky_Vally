using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Interactebel : MonoBehaviour
{
    public UnityEvent interact;
    public void Interact()
    {
        interact.Invoke();
    }
}
