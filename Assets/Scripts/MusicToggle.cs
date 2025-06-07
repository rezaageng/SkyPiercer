using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public AudioSource musicSource;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private Image buttonImage;
    private bool isMuted;

    void Start()
    {
        buttonImage = GetComponent<Image>();

        // Ambil status mute dari PlayerPrefs (opsional, agar bisa disimpan antar scene)
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        UpdateUI();
    }

    public void ToggleSound()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (musicSource != null)
        {
            musicSource.mute = isMuted;
        }

        if (buttonImage != null)
        {
            buttonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
        }
    }
}
