using UnityEngine;

public class ButtonSoundEffect : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}