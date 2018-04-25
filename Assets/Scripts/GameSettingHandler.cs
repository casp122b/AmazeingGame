using UnityEngine;
using UnityEngine.UI;

public class GameSettingHandler : MonoBehaviour {

    public GameObject SettingsHandler;
    public Slider[] volumeSliders;
    public Slider Volume;
    AudioSource masterVolume;
    float masterVolumeSlider;
    public AudioSource music;

    void Update()
    {
        if (Volume)
        {
            music.volume = Volume.value;
        }
    }

    public void SetMasterVolume(float value)
    {
    }

    public void SetMusicVolume(float value)
    {

    }

    public void SetSfxVolume(float value)
    {

    }
}
