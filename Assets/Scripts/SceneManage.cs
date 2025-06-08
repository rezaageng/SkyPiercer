using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public AudioSource buttonAudio; // Drag AudioSource dari Inspector
    public float delay = 0.3f; // Waktu delay sebelum pindah scene

    public void GantiScene()
    {
        StartCoroutine(LoadSceneWithDelay("NamaScene"));
    }

    public void MainMenu()
    {
        StartCoroutine(LoadSceneWithDelay("MainMenu"));
    }

    public void Credit()
    {
        StartCoroutine(LoadSceneWithDelay("Credit"));
    }

    public void gamePlay()
    {
        StartCoroutine(LoadSceneWithDelay("GamePlay1"));
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        if (buttonAudio != null)
        {
            buttonAudio.Play();
        }
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
