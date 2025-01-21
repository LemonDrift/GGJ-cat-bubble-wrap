using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public BubbleController[] bubbles; // Array to hold references to all bubbles in the scene
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
            bubble.SetManager(this);
        }
    }

    public void OnBubblePopped()
    {
        // Increment the popped bubble count
        poppedBubbles++;
        Debug.Log($"Bubbles popped: {poppedBubbles}/{totalBubbles}");

        // Check for win condition
        if (poppedBubbles == totalBubbles)
        {
            gameManager.EndGame(true);
            Debug.Log("All bubbles popped! You win!");
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

        Debug.Log("Bubble wrap reset.");
    }

    
}
