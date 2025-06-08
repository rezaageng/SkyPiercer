using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    public void PlaySound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }
}
