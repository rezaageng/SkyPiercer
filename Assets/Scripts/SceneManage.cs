using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void ContinueGame()
    {
        int highestLevelCompleted = PlayerPrefs.GetInt("HighestLevelCompleted", 0);
        int levelToLoad = highestLevelCompleted + 1;

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

    public void GamePlay()
    {
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