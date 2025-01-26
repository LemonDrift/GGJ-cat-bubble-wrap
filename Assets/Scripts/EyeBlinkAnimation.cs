using UnityEngine;

public class EyeBlinkAnimation : MonoBehaviour
{
    private Animator animator; // Reference to the Animator
    public GameManager gameManager; // Reference to the GameManager for game state

    // Random interval range for triggering the animation
    public float minInterval = 2f;
    public float maxInterval = 5f;

    private void Start()
    {
        // Initialize the Animator component
        animator = GetComponent<Animator>();

        // Check if gameManager is assigned and start the coroutine
        if (gameManager != null)
        {
            StartCoroutine(TriggerAnimationRandomly());
        }
        else
        {
            Debug.LogError("RandomAnimation: GameManager is not assigned in the Inspector!");
        }
    }

    private System.Collections.IEnumerator TriggerAnimationRandomly()
    {
        Debug.Log("RandomAnimation: Starting random animation trigger coroutine");

        // Run the loop as long as the game is active
        while (true)
        {
            // Wait until the game is active
            while (!gameManager.IsGameActive())
            {
                yield return null; // Wait for the next frame
            }

            // Wait for a random interval
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // Trigger the animation
            animator.SetTrigger("CatEyeBlink");
            Debug.Log("RandomAnimation: Triggered PlayAnimationRandom");
        }
    }
}