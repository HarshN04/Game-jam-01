using UnityEngine;
using TMPro; // only required for popup prefab text

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Sugarcane : MonoBehaviour
{
    [Header("Growth Sprites")]
    public Sprite seedSprite;
    public Sprite smallSprite;
    public Sprite normalSprite;

    [Header("Growth Settings")]
    public float growthTime = 5f;
    private float timer;
    public int growthStage = 0; // 0=seed,1=small,2=normal

    [Header("Health Settings")]
    public int maxHP = 10;
    public int currentHP;

    [Header("Popup (optional)")]
    public GameObject popupPrefab;      // world-space TMP prefab (optional)
    public float popupOffsetY = 1f;

    private SpriteRenderer spriteRenderer;

    // player detection
    private bool playerInRange = false;
    private PlayerCombat playerCombat;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHP = maxHP;
        timer = growthTime;
        spriteRenderer.sprite = seedSprite;
    }

    void Update()
    {
        // grow if not fully grown
        if (growthStage < 2)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Grow();
                timer = growthTime;
            }
        }

        // if player is near and clicks
        if (playerInRange && Input.GetMouseButtonDown(0))
        {
            if (growthStage == 2)
            {
                int damage = playerCombat != null ? playerCombat.GetDamage() : 1;
                TakeDamage(damage);
            }
            else
            {
                Debug.Log("Sugarcane not fully grown.");
            }
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
            Debug.Log("Sugarcane fully grown.");
        }
    }

    public void TakeDamage(int damage)
    {
        if (growthStage < 2) return;

        currentHP -= damage;
        // you can add hit feedback here (flash/shake)

        if (currentHP <= 0)
            Harvest();
    }

    void Harvest()
    {
        // If Inventory class uses Instance (capital I)
        if (Inventory.Instance != null)
        {
            bool added = Inventory.Instance.AddSugarcane(1); // expects AddSugarcane to return bool (success)
            if (!added)
            {
                // inventory full -> keep cane fully grown and restore HP
                currentHP = maxHP;
                ShowPopup("Inventory Full!", Color.red);
                Debug.Log("Inventory full! Can't harvest.");
                return;
            }
        }
        else
        {
            Debug.LogWarning("Inventory.Instance is null. Harvesting will still reset the cane.");
            // If Inventory missing, still add nothing and continue to reset
        }

        // success
        ShowPopup("+1", Color.green);
        ResetCane();
        Debug.Log("Sugarcane harvested.");
    }

    void ResetCane()
    {
        currentHP = maxHP;
        growthStage = 0;
        timer = growthTime;
        spriteRenderer.sprite = seedSprite;
    }

    void ShowPopup(string text, Color color)
    {
        if (popupPrefab == null) return;

        Vector3 spawnPos = transform.position + Vector3.up * popupOffsetY;
        GameObject pop = Instantiate(popupPrefab, spawnPos, Quaternion.identity);
        TMP_Text tmp = pop.GetComponentInChildren<TMP_Text>();
        if (tmp != null)
        {
            tmp.text = text;
            tmp.color = color;
        }
        Destroy(pop, 1.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerCombat = other.GetComponent<PlayerCombat>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerCombat = null;
        }
    }

    public void ApplyWateringCan(float multiplier)
{
    timer *= multiplier; // reduce remaining growth time
    growthTime *= multiplier; // reduce overall growth time
    Debug.Log("Sugarcane growth speed boosted!");
}

}
