using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPageManager : MonoBehaviour
{
    public GameObject settingsPanel;
    
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OpenSettings()
    {
        // Logic to open settings (e.g., load a new scene or show a panel)
        Debug.Log("Open Settings");
        settingsPanel.SetActive(true);
    }
    
    public void CloseSettings()
    {
        // Logic to close settings (e.g., load a new scene or hide a panel)
        Debug.Log("Close Settings");
        settingsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        // Logic to open credits (e.g., load a new scene or show a panel)
        Debug.Log("Open Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}