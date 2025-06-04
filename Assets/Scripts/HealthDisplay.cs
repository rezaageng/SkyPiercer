using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Sprite emptyHealth;
    public Sprite fullHealth;
    public Image[] hearts;

    public PlayerHealth playerHealth; // Referensi ke script PlayerHealth

    void Start()
    {
        // Set tampilan awal sesuai health awal
        SetHealth(playerHealth.health);
    }

    // Method untuk update tampilan hati berdasarkan nilai health
    public void SetHealth(int health)
    {
        int maxHealth = playerHealth.maxHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
                hearts[i].sprite = i < health ? fullHealth : emptyHealth;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
