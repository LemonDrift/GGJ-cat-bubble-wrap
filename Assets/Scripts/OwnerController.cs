using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OwnerController : MonoBehaviour
{
    // Reference to GameManager
    public GameManager gameManager;

    // State properties
    [SerializeField] private TextMeshProUGUI ownerStatusText;
    private bool isTurningBack = false; // Tracks if the owner is turning back

    // Timer properties
    // TODO(lydia): adjust this when further adjustments are made
    public float minCheckInterval = 3.0f; // Minimum time before the owner checks
    public float maxCheckInterval = 6.0f; // Maximum time before the owner checks

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

        // TODO(lydia): this is for demo purpose only
        // Show the "Turning" text
        if (ownerStatusText != null)
        {
            ownerStatusText.gameObject.SetActive(true);
        }
        
        // Simulate the owner turning back
        CheckCat();

        // After checking, simulate the owner turning away
        StartCoroutine(TurnAwayAfterCheck());
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

    private IEnumerator TurnAwayAfterCheck()
    {
        // Simulate a delay for turning back
        yield return new WaitForSeconds(1.0f);

        // Owner stops checking
        isTurningBack = false;
        
        // TODO(lydia): Demo purpose only
        // Hide the "Turning" text
        if (ownerStatusText != null)
        {
            ownerStatusText.gameObject.SetActive(false);
        }
    }

    public bool IsOwnerChecking()
    {
        return isTurningBack;
    }
}
