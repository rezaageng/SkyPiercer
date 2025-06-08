using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for UI components like Button

public class SceneManage : MonoBehaviour
{
    public Button continueButton; // Assign your "Continue" button from the Inspector

    void Start()
    {
        // Check if a level has been completed.
        int highestLevelCompleted = PlayerPrefs.GetInt("HighestLevelCompleted", 0);

        if (highestLevelCompleted == 0)
        {
            // If no level has been completed, hide the continue button.
            if (continueButton != null)
            {
                continueButton.gameObject.SetActive(false);
            }
        }
        else
        {
            // If a level has been completed, show the continue button.
            if (continueButton != null)
            {
                continueButton.gameObject.SetActive(true);
            }
        }
    }

    // This function will be called by your "Continue" button
    public void ContinueGame()
    {
        int highestLevelCompleted = PlayerPrefs.GetInt("HighestLevelCompleted", 0);
        int levelToLoad = highestLevelCompleted + 1;

        // If all levels are completed, loop back to level 1
        if (levelToLoad > 3)
        {
            levelToLoad = 1;
        }

        string sceneToLoad = "GamePlay" + levelToLoad;

        if (Application.CanStreamedLevelBeLoaded(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene " + sceneToLoad + " not found. Loading GamePlay1 instead.");
            SceneManager.LoadScene("GamePlay1");
        }
    }

    // This function can be used for a "New Game" button
    public void gamePlay()
    {
        // Optional: Reset progress when starting a new game
        PlayerPrefs.DeleteKey("HighestLevelCompleted");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GamePlay1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }
}