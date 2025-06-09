using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public int maxHealth = 3;
    public void TakeDamage(int amount)
    {
        health = Mathf.Clamp(health - amount, 0, maxHealth);
    }
}
