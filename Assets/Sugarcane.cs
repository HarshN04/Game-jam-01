using UnityEngine;

public class Sugarcane : MonoBehaviour
{
    [Header("Growth Sprites")]
    public Sprite seedSprite;
    public Sprite smallSprite;
    public Sprite normalSprite;

    [Header("Growth Settings")]
    public float growthTime = 5f; // time per stage
    private float timer;
    public int growthStage = 0; // 0 = seed, 1 = small, 2 = normal

    [Header("Health Settings")]
    public int maxHP = 10;
    public int currentHP;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ResetCane();
    }

    private void Update()
    {
        // Only grow if not destroyed and not fully grown
        if (growthStage < 2)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Grow();
                timer = growthTime;
            }
        }
    }

    // Handle growth stages
    void Grow()
    {
        if (growthStage == 0)
        {
            spriteRenderer.sprite = smallSprite;
            growthStage = 1;
        }
        else if (growthStage == 1)
        {
            spriteRenderer.sprite = normalSprite;
            growthStage = 2;
        }
        else if (growthStage == 2)
        {
            Debug.Log("Sugarcane is fully grown!");
        }
    }

    // Irrigation boost cuts time in half
    public void Irrigate()
    {
        timer /= 2f;
        Debug.Log("Irrigation applied, growth speed boosted!");
    }

    // Take damage when hit
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Harvest();
        }
    }

    // When destroyed/harvested
    void Harvest()
    {
        Debug.Log("Sugarcane harvested!");

        // Reset to seed stage
        ResetCane();
    }

    // Reset HP + Growth back to seed stage
    void ResetCane()
    {
        currentHP = maxHP;
        growthStage = 0;
        spriteRenderer.sprite = seedSprite;
        timer = growthTime;
    }
}
