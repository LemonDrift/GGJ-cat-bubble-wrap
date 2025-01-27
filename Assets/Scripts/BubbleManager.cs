using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public BubbleController[] bubbles; // Array to hold references to all bubbles in the scene
    public Sprite[] poppedBubbleSprites; // Array to hold all possible bubble sprites
    public Sprite originalBubbleSprite; // Reference to the original bubble sprite
    public CatController catController; // Reference to the CatController
    public AudioSource audioSource;
    public AudioClip delayedPopSound; // Assign the delayed pop sound in Inspector
    private int totalBubbles;
    private int poppedBubbles;
    
    public GameManager gameManager;

    private void Start()
    {
        // Initialize bubble data
        totalBubbles = bubbles.Length;
        poppedBubbles = 0;

        // Assign the manager reference to each bubble
        foreach (BubbleController bubble in bubbles)
        {
            if (bubble == null)
            {
                Debug.LogError("BubbleManager: A bubble in the array is null!");
                continue;
            }

            if (bubble.GetComponent<SpriteRenderer>() == null)
            {
                Debug.LogError($"BubbleManager: {bubble.name} is missing a SpriteRenderer!");
                continue;
            }
            
            Sprite[] poppedSprites = GetPoppedBubbleSprites();
            bubble.SetManager(this, originalBubbleSprite, poppedSprites);
        }
    }

    private Sprite[] GetPoppedBubbleSprites()
    {
        return poppedBubbleSprites; // Expose it as read-only
    }
    
    public void OnBubblePopped()
    {
        // Play a sound after a delay
        PlaySoundWithDelay(0.1f);
        
        // Increment the popped bubble count
        poppedBubbles++;
        Debug.Log($"Bubbles popped: {poppedBubbles}/{totalBubbles}");

        // Check for win condition
        if (poppedBubbles >= totalBubbles)
        {
            gameManager.EndGame(true);
        }
    }
    
    
    public void ResetBubbleWrap()
    {
        // Reset popped bubbles count
        poppedBubbles = 0;

        // Reset each bubble in the grid
        foreach (BubbleController bubble in bubbles)
        {
            bubble.ResetBubble(); // Ensure BubbleController has a ResetBubble method
        }
    }

    public void PlaySoundWithDelay(float delay)
    {
        StartCoroutine(PlaySoundAfterDelayCoroutine(delay));
    }

    private IEnumerator PlaySoundAfterDelayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (audioSource != null && delayedPopSound != null)
        {
            audioSource.PlayOneShot(delayedPopSound);
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip is missing!");
        }
    }
}
