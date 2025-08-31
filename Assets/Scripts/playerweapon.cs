using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public static PlayerWeapon Instance;

    public string currentWeapon = "Barefist";
    public int damage = 1;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetWeapon(string weaponName, int weaponDamage)
    {
        currentWeapon = weaponName;
        damage = weaponDamage;
        Debug.Log($"Weapon changed to {weaponName} with {damage} damage");
    }
}
