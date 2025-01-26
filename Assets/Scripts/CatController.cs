using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    // Reference to GameManager and BubbleManager
    public GameManager gameManager;
    public BubbleManager bubbleManager;
    public OwnerController ownerController;
    public AudioClip scratchingSound; // Assign the scratching sound in Inspector
    public AudioSource audioSource;
    
    public Animator lowerLeftPawAnimator;
    
    private bool canPerformAction = true;

    public static CatController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
    
    public void PerformPawAction(BubbleController bubble = null)
    {
        // Check if the owner is currently checking on the cat
        if (gameManager != null && ownerController.IsOwnerChecking())
        {
            gameManager.EndGame(false);
            return;
        }
        
        // Adjust target position in y axis to match the bubble's position
        Vector3 targetPosition = bubble != null ? bubble.transform.position : Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.y -= 450;
        StartCoroutine(MovePawCoroutine(targetPosition, bubble));
    }

    private IEnumerator MovePawCoroutine(Vector3 targetPosition, BubbleController bubble)
    {
        // trigger the lower left cat's paw move
        lowerLeftPawAnimator.SetTrigger("PawMoveTrigger");
        
        canPerformAction = false;
        Vector3 originalPosition = transform.position;
        float moveDuration = 0.5f; // Total time for the paw to move to the target
        float elapsedTime = 0f;

        // Move the paw to the target position
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // Add scratching sound effect
        audioSource.PlayOneShot(scratchingSound);
        
        // If there's a bubble, notify it to pop. Add loggings
        if (bubble != null)
        {
            bubble.PopBubble();
        }
        else
        {
            Debug.Log("Cat paw action performed! No bubble to pop.");
        }
        
        // Move the paw back to the original position
        elapsedTime = 0f; // Reset elapsed time for the return trip
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition; // Ensure it snaps back exactly to the original position
        
        // Ready to receive the next popping action
        canPerformAction = true;
    }
    
    public bool IsCatPoppingBubbles()
    {
        return !canPerformAction;
    }
    
    public void Start()
    {
        // TODO(lydia): we need to seperate canPerformAction from gameStart flag
        canPerformAction = true; // Allow the cat to interact with bubbles
        transform.position = new Vector3(120, -900, 0);
        Debug.Log("Cat interaction enabled.");
    }

    public void DisableCatInteraction()
    {
        // TODO(lydia): we need to seperate canPerformAction from gameStart flag
        canPerformAction = false; // Prevent the cat from interacting with bubbles
        Debug.Log("Cat interaction disabled.");
    }
}
