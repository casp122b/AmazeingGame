using UnityEngine;
using UnityEngine.UI;

public class GameSettingHandler : MonoBehaviour
{

    public Slider Volume;
    public AudioSource music;

    void Update()
    {
        if (Volume)
        {
            music.volume = Volume.value;
        }
    }
}
