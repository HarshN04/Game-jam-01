using UnityEngine;

public class Cane : MonoBehaviour
{
    public int maxHP = 10;   // total HP
    private int currentHP;

    void Start()
    {
        currentHP = maxHP;   // start full
    }

    // Call this when the player attacks
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Here you can play animation, spawn items, etc.
        Debug.Log("Cane destroyed!");

        Destroy(gameObject); // remove cane from scene
    }
}
