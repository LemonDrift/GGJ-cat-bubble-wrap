using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    private BubbleManager manager; // Reference to the BubbleManager
    private bool isPopped = false;
    
    public void SetManager(BubbleManager bubbleManager)
    {
        manager = bubbleManager; // Assign the manager reference
    }
    
    private void OnMouseDown()
    {
        // Trigger bubble pop logic when clicked
        if (!isPopped)
        {
            PopBubble();
        }
    }
    
    public void PopBubble()
    {
        isPopped = true;
        Debug.Log($"{name} popped!");
        
        // Optional: Visual feedback for popping
        GetComponent<SpriteRenderer>().color = Color.gray; // Example: Change color
        GetComponent<Collider2D>().enabled = false; // Disable further interactions
        
        // Notify the manager
        if (manager != null)
        {
            manager.OnBubblePopped();
        }
    }
    
    public void ResetBubble()
    {
        // Reset the popped state
        isPopped = false;

        // Reset the visual feedback
        GetComponent<SpriteRenderer>().color = Color.blue; // Reset color to original (or any default color)

        // Re-enable the collider for interaction
        GetComponent<Collider2D>().enabled = true;

        Debug.Log($"{name} has been reset.");
    }

}

