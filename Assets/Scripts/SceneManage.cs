using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManage : MonoBehaviour
{
    public Button continueButton;

    void Start()
    {
        int highestLevelCompleted = PlayerPrefs.GetInt("HighestLevelCompleted", 0);

        if (highestLevelCompleted == 0)
        {
            if (continueButton != null)
            {
                continueButton.gameObject.SetActive(false);
            }
        }
        else
        {
            if (continueButton != null)
            {
                continueButton.gameObject.SetActive(true);
            }
        }
    }

    public void ContinueGame()
    {
        StartCoroutine(LoadContinueGame());
    }

    private IEnumerator LoadContinueGame()
    {
        yield return new WaitForSeconds(0.3f);

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

    public void gamePlay()
    {
        StartCoroutine(LoadGamePlay());
    }

    private IEnumerator LoadGamePlay()
    {
        yield return new WaitForSeconds(0.3f);
        PlayerPrefs.DeleteKey("HighestLevelCompleted");
        PlayerPrefs.Save();
        SceneManager.LoadScene("GamePlay1");
    }

    public void MainMenu()
    {
        StartCoroutine(LoadSceneWithDelay("MainMenu"));
    }

    public void Credit()
    {
        StartCoroutine(LoadSceneWithDelay("Credit"));
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(sceneName);
    }
}
