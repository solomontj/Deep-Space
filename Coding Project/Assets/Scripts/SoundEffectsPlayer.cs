using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource doorSoundEffect;

    void Start()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }

    void Update()
    {
        
    }

        public void PlayDoorSoundEffect()
    {
        if (doorSoundEffect != null)
        {
            doorSoundEffect.Play();
        }
    }

}
