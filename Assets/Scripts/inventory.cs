using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance; // Singleton reference

    [Header("Inventory Settings")]
    public int maxCapacity = 10;
    public int sugarcaneCount = 0;

    [Header("Currency")]
    public int money = 0;
    public int sugarcaneSellPrice = 5; // Rs per sugarcane

    [Header("UI References")]
    public TMP_Text sugarcaneText; // Assign InventoryPanel -> SugarcaneText
    public TMP_Text moneyText;     // Assign MoneyPanel -> MoneyText

    private void Awake()
    {
        // Ensure singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    /// <summary>
    /// Adds sugarcane to inventory. Returns false if inventory full.
    /// </summary>
    public bool AddSugarcane(int amount)
    {
        if (sugarcaneCount + amount > maxCapacity)
        {
            Debug.Log("Inventory full!");
            return false; // Cannot add more
        }

        sugarcaneCount += amount;
        UpdateUI();
        return true;
    }

    /// <summary>
    /// Removes sugarcane from inventory.
    /// </summary>
    public void RemoveSugarcane(int amount)
    {
        sugarcaneCount = Mathf.Max(0, sugarcaneCount - amount);
        UpdateUI();
    }

    /// <summary>
    /// Sell all sugarcane in inventory.
    /// </summary>
    public void SellAllSugarcane()
    {
        if (sugarcaneCount > 0)
        {
            int totalEarned = sugarcaneCount * sugarcaneSellPrice;
            money += totalEarned;
            sugarcaneCount = 0;

            Debug.Log($"Sold sugarcane for Rs {totalEarned}. Current money: Rs {money}");
        }
        else
        {
            Debug.Log("No sugarcane to sell!");
        }

        UpdateUI();
    }

    /// <summary>
    /// Updates all UI texts (sugarcane count and money)
    /// </summary>
    public void UpdateUI()
    {
        if (sugarcaneText != null)
            sugarcaneText.text = $"{sugarcaneCount}/{maxCapacity}";

        if (moneyText != null)
            moneyText.text = $"Rs: {money}";
    }
}
