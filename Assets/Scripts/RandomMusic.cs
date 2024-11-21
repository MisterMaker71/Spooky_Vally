using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomMusic : MonoBehaviour
{
    public List<AudioClip> songs = new List<AudioClip>();
    public Vector2 PauseRange;
    float pauseTime;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        pauseTime = Random.Range(PauseRange.x, PauseRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(source != null)
        {
            if (!source.isPlaying)
            {
                pauseTime -= Time.deltaTime;
                if(pauseTime <= 0)
                {
                    pauseTime = Random.Range(PauseRange.x, PauseRange.y);
                    PlyRandom();
                }
            }
        }
    }
    public void PlyRandom()
    {
        songs.RemoveAll(song => song == null);
        if (source != null)
        {
            if (songs.Count > 0)
            {
                source.clip = songs[Random.Range(0, songs.Count)];
                source.Play();
            }
        }
    }
}
