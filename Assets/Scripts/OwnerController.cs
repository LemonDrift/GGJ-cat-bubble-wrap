using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OwnerController : MonoBehaviour
{
    // Reference to GameManager
    public GameManager gameManager;
    public Animator ownerAnimator;

    // State properties
    [SerializeField] private TextMeshProUGUI ownerStatusText;
    private bool isTurningBack = false; // Tracks if the owner is turning back

    // Timer properties
    public float minCheckInterval = 3.0f; // Minimum time before the owner checks
    public float maxCheckInterval = 6.0f; // Maximum time before the owner checks
    public float ownerAnimationLength = 1.0f; // This need to match with the actual animation length!
    
    private Coroutine turnBackCoroutine;

    private void Start()
    {
        // Start the owner's random check cycle
        StartOwnerActivity();
    }

    public void StartOwnerActivity()
    {
        // Start the coroutine for random checks
        if (turnBackCoroutine == null)
        {
            turnBackCoroutine = StartCoroutine(RandomTurnBack());
        }
    }

    public void StopOwnerActivity()
    {
        // Stop the owner's activity
        if (turnBackCoroutine != null)
        {
            StopCoroutine(turnBackCoroutine);
            turnBackCoroutine = null;
        }
    }

    private IEnumerator RandomTurnBack()
    {
        while (true)
        {
            // Wait for a random interval
            float waitTime = Random.Range(minCheckInterval, maxCheckInterval);
            yield return new WaitForSeconds(waitTime);

            // Turn back to check the cat
            TurnBack();
        }
    }

    private void TurnBack()
    {
        if (isTurningBack) return;
        isTurningBack = true;
        
        if (ownerAnimator != null)
        {
            ownerAnimator.SetTrigger("OwnerTurningTrigger");
            StartCoroutine(WaitForAnimationToEnd("OwnerTurningTrigger"));
        }
        else
        {
            Debug.LogError("OwnerAnimator is not assigned in the Inspector.");
        }

        // After checking, set turning indicator to false
        isTurningBack = false;
    }
    
    private IEnumerator WaitForAnimationToEnd(string animationName)
    {
        Debug.LogWarning("))(@e@@@!@OwnerController: Waiting for animation to end.");
        // Wait for the animation to finish
        yield return new WaitForSeconds(ownerAnimationLength);
        // Debug.LogWarning("!@##StateInfo.length: " + stateInfo.length);

        // Call CheckCat after the animation finishes
        CheckCat();
        isTurningBack = false; // Reset the turning indicator
    }

    private void CheckCat()
    {
        if (gameManager != null)
        {
            // Check if the cat is actively popping bubbles
            if (CatController.Instance.IsCatPoppingBubbles())
            {
                Debug.Log("Owner caught the cat popping bubbles!");
                gameManager.EndGame(false);
            }
            else
            {
                Debug.Log("Owner turned back but did not catch the cat.");
            }
        }
        else
        {
            Debug.LogWarning("OwnerController is missing a reference to GameManager!");
        }
    }

    public bool IsOwnerChecking()
    {
        Debug.Log("!!@@@@isTurningBack: " + isTurningBack);
        return isTurningBack;
    }
}
