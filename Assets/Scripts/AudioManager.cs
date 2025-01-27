using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; } // Singleton instance

    [SerializeField] private Slider bgmSlider; // Slider for BGM volume
    [SerializeField] private Slider sfxSlider; // Slider for SFX volume
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
        // Load saved volume levels or set defaults
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Apply saved settings to audio sources
        ApplyVolumeSettings();

        // Initialize sliders with saved values
        if (bgmSlider != null)
        {
            bgmSlider.value = bgmVolume;
            bgmSlider.onValueChanged.AddListener(SetBGMVolume); // Add listener for changes
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVolume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume); // Add listener for changes
        }
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume); // Save to PlayerPrefs
        UpdateBGMVolume(); // Apply to all BGM sources
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume); // Save to PlayerPrefs
        UpdateSFXVolume(); // Apply to all SFX sources
    }

    private void ApplyVolumeSettings()
    {
        UpdateBGMVolume();
        UpdateSFXVolume();
    }

    private void UpdateBGMVolume()
    {
        foreach (AudioSource bgmSource in FindObjectsOfType<AudioSource>())
        {
            if (bgmSource.CompareTag("BGM")) // Ensure the AudioSource has the BGM tag
            {
                bgmSource.volume = bgmVolume;
            }
        }
    }

    private void UpdateSFXVolume()
    {
        foreach (AudioSource sfxSource in FindObjectsOfType<AudioSource>())
        {
            if (sfxSource.CompareTag("SFX")) // Ensure the AudioSource has the SFX tag
            {
                sfxSource.volume = sfxVolume;
            }
        }
    }
}
