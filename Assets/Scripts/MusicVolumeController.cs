
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    public Slider slider;

    public void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume",0.8f);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void changeVolume()
    {
        AudioListener.volume = slider.value;
        Save();
    }

    public void Load()
    {
        slider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", slider.value);
    }
}