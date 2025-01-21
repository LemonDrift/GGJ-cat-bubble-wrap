using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    // Reference to GameManager and BubbleManager
    public GameManager gameManager;
    public BubbleManager bubbleManager;
    public OwnerController ownerController;
    
    // Cooldown variables
    // TODO(lydia): adjust this when further adjustments are made
    public float actionCooldown = 0.5f; 
    private bool canPerformAction = true;
    
    private void Update()
    {
        // Check for player input (mouse click or touch)
        if (Input.GetMouseButtonDown(0) && canPerformAction)
        {
            PerformPawAction();
        }
    }

    private void PerformPawAction()
    {
        // Check if the owner is currently checking on the cat
        if (gameManager != null && ownerController.IsOwnerChecking())
        {
            Debug.Log("Game Over: Owner caught the cat trying to pop bubbles!");
            gameManager.EndGame(false);
            return;
        }
        
        // If safe, perform the paw action
        Debug.Log("Cat paw action performed!");
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        canPerformAction = false;
        yield return new WaitForSeconds(actionCooldown);
        canPerformAction = true;
    }

    public bool IsCatPoppingBubbles()
    {
        return !canPerformAction;
    }
    
    public void EnableCatInteraction()
    {
        // TODO(lydia): we need to seperate canPerformAction from gameStart flag
        canPerformAction = true; // Allow the cat to interact with bubbles
        Debug.Log("Cat interaction enabled.");
    }

    public void DisableCatInteraction()
    {
        // TODO(lydia): we need to seperate canPerformAction from gameStart flag
        canPerformAction = false; // Prevent the cat from interacting with bubbles
        Debug.Log("Cat interaction disabled.");
    }
}
