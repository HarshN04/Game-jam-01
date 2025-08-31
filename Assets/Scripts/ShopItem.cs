using UnityEngine;
using UnityEngine.UI;

public enum ShopItemType
{
    Weapon,
    Backpack,
    WateringCan
}

public class ShopItem : MonoBehaviour
{
    public ShopItemType itemType;

    [Header("Weapon Option")]
    public PlayerCombat.WeaponType weaponType;

    [Header("Backpack Option")]
    public int newInventoryCapacity = 10;

    [Header("Watering Can Option")]
    public float growthMultiplier = 0.5f;

    public int price = 10;

    private Button button;
    private Text buttonText;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<Text>();
        button.onClick.AddListener(Purchase);

        // Check if weapon is already owned at start
        if (itemType == ShopItemType.Weapon)
        {
            PlayerCombat pc = FindObjectOfType<PlayerCombat>();
            if (pc != null && pc.currentWeapon == weaponType)
                SetAsPurchased();
        }
    }

    void Purchase()
    {
        if (Inventory.Instance.money < price)
        {
            Debug.Log("Not enough money!");
            return;
        }

        Inventory.Instance.money -= price;

        switch (itemType)
        {
            case ShopItemType.Weapon:
                PlayerCombat pc = FindObjectOfType<PlayerCombat>();
                if (pc != null)
                    pc.currentWeapon = weaponType;
                Debug.Log($"{weaponType} purchased!");
                break;

            case ShopItemType.Backpack:
                Inventory.Instance.maxCapacity = newInventoryCapacity;
                Debug.Log($"Inventory upgraded to {newInventoryCapacity} slots!");
                break;

            case ShopItemType.WateringCan:
                Sugarcane[] allCane = FindObjectsOfType<Sugarcane>();
                foreach (var cane in allCane)
                    cane.growthTime = cane.growthTime * growthMultiplier;
                Debug.Log($"Watering can purchased! Growth speed increased.");
                break;
        }

        SetAsPurchased();
        Inventory.Instance.UpdateUI(); // refresh UI
    }

    void SetAsPurchased()
    {
        // Disable button so it cannot be clicked again
        if (button != null)
            button.interactable = false;

        // Change button text to "Owned" if you want
        if (buttonText != null)
            buttonText.text = "Owned";
    }
}
