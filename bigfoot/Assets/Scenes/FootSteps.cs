using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public List<AudioClip> sounds;
    public AudioSource src;

    public float lowRange = 0.4f;
    public float highRange = 0.7f;
    public float lowPitch = 0.7f;
    public float highPitch = 1.2f;
    public void PlayRandomFootStep()
    {
        src.clip = sounds[Random.Range(0, sounds.Count)];
        src.volume = Random.Range(lowRange, highRange);
        src.pitch = Random.Range(lowPitch, highPitch);
        src.Play();
    }

}
