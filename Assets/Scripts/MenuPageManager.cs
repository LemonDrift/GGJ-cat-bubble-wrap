using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPageManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OpenSettings()
    {
        // Logic to open settings (e.g., load a new scene or show a panel)
        Debug.Log("Open Settings");
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