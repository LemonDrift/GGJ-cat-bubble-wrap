using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // References to other controllers
    public OwnerController ownerController;
    public CatController catController;
    public BubbleManager bubbleManager;
    
    // TODO(remove): import the text TMP
    public TMPro.TextMeshProUGUI ownerStatusText;
    public GameObject winPopup;
    public TMPro.TextMeshProUGUI gameOverText;
    
    // Game state properties
    // private int poppedBubblesCount = 0;
    private bool isGameActive = false;
    
    private void Start()
    {
        // Initialize the game
        StartGame();
        Debug.Log("OwnerStatusText active: " + ownerStatusText.gameObject.activeSelf);

    }
    
    public void StartGame()
    {
        // Reset variables and start the game
        isGameActive = true;
        
        // Notify other components to initialize their states
        ownerController.StartOwnerActivity();
        catController.EnableCatInteraction();
        bubbleManager.ResetBubbleWrap();
    }
    
    public void EndGame(bool win = false)
    {
        // End the game and trigger game-over logic
        isGameActive = false;
        
        // Notify other components
        ownerController.StopOwnerActivity();
        catController.DisableCatInteraction();

        Debug.Log("Game Over! " + (win ? "You win!" : "You lose!"));

        // TODO(lydia): Add game-over screen with winning/losing message
        gameOverText.text = win ? "You win!" : "You lose!";
        winPopup.SetActive(true);
        
        
    }
    
    // public void IncrementPoppedBubbles()
    // {
    //     // Increment the counter for popped bubbles
    //     poppedBubblesCount++;
    //     Debug.Log("Bubbles Popped: " + poppedBubblesCount);
    //     
    //     // Check for win conditions
    //     if (poppedBubblesCount >= bubbleManager.GetTotalBubbleCount())
    //     {
    //         EndGame();
    //     }
    // }
    public void RestartGame()
    {
        // Reset game variables if needed
        Debug.Log("Game restarting...");
        
        // deactivete winPopup
        winPopup.SetActive(false);
    
        // Call StartGame to reset and begin the game
        StartGame();
    }

}

