using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DestroyOnAudioEnd : MonoBehaviour
{
    AudioSource source;
    bool hasPlayed = false;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (source != null)
        {
            if (source.isPlaying && !hasPlayed)
            {
                hasPlayed = true;
            }
            if (!source.isPlaying && hasPlayed)
            {
                Destroy(gameObject);
            }
        }
    }
}
