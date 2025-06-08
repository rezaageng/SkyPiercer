using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Sprite emptyHealth;
    public Sprite fullHealth;
    public Image[] hearts;

    public PlayerHealth playerHealth;

    void Start()
    {
        SetHealth(playerHealth.health);
    }

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
