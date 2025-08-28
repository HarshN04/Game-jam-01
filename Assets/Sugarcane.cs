using UnityEngine;

public class Sugarcane : MonoBehaviour
{
    public Sprite seedSprite;
    public Sprite smallSprite;
    public Sprite normalSprite;

    private SpriteRenderer spriteRenderer;
    private int growthStage = 0; // 0 = seed, 1 = small, 2 = normal
    private float growthTime = 5f; // default 5 seconds per stage
    private float timer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = seedSprite;
        timer = growthTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Grow();
            timer = growthTime;
        }
    }

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
}
