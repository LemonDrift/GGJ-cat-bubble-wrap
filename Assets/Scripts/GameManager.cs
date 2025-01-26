using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // References to other controllers
    public OwnerController ownerController;
    public BubbleManager bubbleManager;
    
    public TMPro.TextMeshProUGUI ownerStatusText;
    public GameObject winPopup;
    public TMPro.TextMeshProUGUI gameOverText;
    
    public AudioSource bgmSource;
    public AudioClip bgmClip; // Assign the BGM audio clip in Inspector
    public AudioClip winSound; // Assign this in the Inspector
    public AudioClip loseSound; // Assign this in the Inspector
    public AudioSource sfxSource; // Assign this in the Inspector (can use the same AudioSource or a new one)
    
    // Game state properties
    // private int poppedBubblesCount = 0;
    private bool isGameActive = false;
    
    private void Start()
    {
        // Initialize the game
        StartGame();
        Debug.Log("OwnerStatusText active: " + ownerStatusText.gameObject.activeSelf);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left mouse click
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    
            // Perform a 2D raycast to detect the clicked collider
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
    
            if (hit.collider != null)
            {
                // Check if it's a bubble
                BubbleController bubble = hit.collider.GetComponent<BubbleController>();
                if (bubble != null)
                {
                    bubble.OnMouseDown(); // Manually call bubble's OnMouseDown
                    return; // Stop further processing
                }
    
                // Check if it's the background
                BackgroundClickHandler background = hit.collider.GetComponent<BackgroundClickHandler>();
                if (background != null)
                {
                    background.OnMouseDown(); // Manually call background's OnMouseDown
                }
            }
        }
    }

    public void StartGame()
    {
        // Reset variables and start the game
        isGameActive = true;
        
        // Play the background music
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.Play();
        
        // Notify other components to initialize their states
        ownerController.StartOwnerActivity();
        CatController.Instance.Start();
        bubbleManager.ResetBubbleWrap();
    }
    
    public void EndGame(bool win = false)
    {
        // End the game and trigger game-over logic
        isGameActive = false;
        
        // Notify other components
        ownerController.StopOwnerActivity();
        CatController.Instance.DisableCatInteraction();
        
        Debug.Log("Game Over! " + (win ? "You win!" : "You lose!"));
        gameOverText.text = win ? "You Win!" : "You Lose!";
        winPopup.SetActive(true);
        
        if (sfxSource != null)
        {
            sfxSource.Stop();
            sfxSource.clip = win ? winSound : loseSound;
            sfxSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource is not assigned in the Inspector.");
        }
        
        bgmSource.Stop();
    }
    
    public void RestartGame()
    {
        // Reset game variables if needed
        Debug.Log("Game restarting...");
        
        // deactivete winPopup
        winPopup.SetActive(false);
    
        // Call StartGame to reset and begin the game
        StartGame();
    }
    
    // Getter for isGameActive
    public bool IsGameActive()
    {
        return isGameActive;
    }

}

