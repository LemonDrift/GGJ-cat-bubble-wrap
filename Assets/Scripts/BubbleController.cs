using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    private BubbleManager _manager; // Reference to the BubbleManager
    private Sprite[] _poppedSprites; // List of bubble sprites to use
    private Sprite _originalSprite; 
    private bool _isPopped = false;
    
    
    public void SetManager( BubbleManager bubbleManager, Sprite bubbleSprite, Sprite[] poppedBubbleSprites)
    {
        _manager = bubbleManager;
        _poppedSprites = poppedBubbleSprites;
        _originalSprite = bubbleSprite;
    }
    
    public void OnMouseDown()
    {
        // Block input if the cat is performing an action
        if (CatController.Instance.IsCatPoppingBubbles())
        {
            return;
        }
        
        // Trigger bubble pop logic when clicked
        if (!_isPopped)
        {
            CatController.Instance.PerformPawAction(this);
            // log the bubble name
            Debug.Log($"BubbleController.OnMouseDown():: Bubble: {name}");
        }
        else
        {
            CatController.Instance.PerformPawAction(null);
            Debug.Log($"BubbleController.OnMouseDown():: Bubble: {name} is already popped.");
        }
    }
    
    public void PopBubble()
    {
        _isPopped = true;
        Debug.Log($"{name} popped!");
        
        // Visual feedback for popping
        GetComponent<SpriteRenderer>().sprite = _poppedSprites[Random.Range(0, _poppedSprites.Length)];
        GetComponent<Collider2D>().enabled = false; // Disable further interactions
        
        // Notify the manager
        if (_manager != null)
        {
            _manager.OnBubblePopped();
        }
    }
    
    public void ResetBubble()
    {
        // Reset the popped state
        _isPopped = false;

        // Reset the visual feedback
        if (_originalSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = _originalSprite;
        }
        else
        {
            // log errors with some information
            Debug.LogError($"Bubble {name} is missing the original sprite reference.");
        }

        // Re-enable the collider for interaction
        GetComponent<Collider2D>().enabled = true;
    }

}

