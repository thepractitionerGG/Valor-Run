using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public  float LobbyMusic;
    public  float InGameMusic;
    public  float SoundVolume;

    public Slider _lobbyMusicSlider;
    public Slider _inGameMusicSlider;
    public Slider _SoundVolumeSlider;

    public static AudioSettings audioSettings;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("SoundVolume"))
        {
            PlayerPrefs.SetFloat("SoundVolume", 0.4f);
            PlayerPrefs.SetFloat("InGameMusic", 0.4f);
            PlayerPrefs.SetFloat("LobbyMusic", 0.4f);
            PlayerPrefs.Save();
        }
    }
    private void Start()
    {
        audioSettings = this;
        _lobbyMusicSlider.onValueChanged.AddListener(LobbyMusicVolumeChange);
        _inGameMusicSlider.onValueChanged.AddListener(InGameMusicVolumeChange);
        _SoundVolumeSlider.onValueChanged.AddListener(SoundVolumeChange);

        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            _lobbyMusicSlider.value = PlayerPrefs.GetFloat("LobbyMusic");
            LobbyMusic = PlayerPrefs.GetFloat("LobbyMusic");

            _inGameMusicSlider.value = PlayerPrefs.GetFloat("InGameMusic");
            InGameMusic = PlayerPrefs.GetFloat("InGameMusic");

            _SoundVolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume");
            SoundVolume = PlayerPrefs.GetFloat("SoundVolume");
        }
    }

    // save the values in the system;
    public void LobbyMusicVolumeChange(float volume)
    {
        LobbyMusic = _lobbyMusicSlider.value;
        PlayerPrefs.SetFloat("LobbyMusic", LobbyMusic);
    }

    public void InGameMusicVolumeChange(float volume)
    {
        InGameMusic = _inGameMusicSlider.value;
        PlayerPrefs.SetFloat("InGameMusic", InGameMusic);
    }

    public void SoundVolumeChange(float volume)
    {
        SoundVolume = _SoundVolumeSlider.value;
        PlayerPrefs.SetFloat("SoundVolume", SoundVolume);
    }
}
