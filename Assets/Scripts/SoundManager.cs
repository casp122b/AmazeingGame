using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;           //A reference to the audio source which will play the sound effects.
    public AudioSource musicSource;         //A reference to the audio source which will play the music.
    public static SoundManager instance = null;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading the scene.
        DontDestroyOnLoad(gameObject);
    }

    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of the array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);
        //Choose a random pitch to play back the clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;          //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.clip = clips[randomIndex];    //Set the clip to the clip at our randomly chosen index.
        efxSource.Play();                       //Play the clip.
    }
}
