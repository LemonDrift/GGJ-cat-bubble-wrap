using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } // Singleton instance

    [SerializeField] private Slider bgmSlider; // Slider for BGM volume (optional)
    [SerializeField] private Slider sfxSlider; // Slider for SFX volume (optional)
    private float bgmVolume = 1f;
    private float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    private void Start()
    {
        // Load saved volume levels
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        ApplyVolumeSettings();
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
        ApplyVolumeSettings();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
        ApplyVolumeSettings();
    }

    private void ApplyVolumeSettings()
    {
        // Update all BGM sources
        foreach (AudioSource bgmSource in FindObjectsOfType<AudioSource>())
        {
            if (bgmSource.CompareTag("BGM")) // Ensure the AudioSource has the BGM tag
            {
                bgmSource.volume = bgmVolume;
            }
        }

        // Update all SFX sources
        foreach (AudioSource sfxSource in FindObjectsOfType<AudioSource>())
        {
            if (sfxSource.CompareTag("SFX")) // Ensure the AudioSource has the SFX tag
            {
                sfxSource.volume = sfxVolume;
            }
        }
    }
}