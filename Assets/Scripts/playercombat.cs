using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public enum WeaponType { Barefist, Rustyknife, Sickle, Scythe, Machete }
    public WeaponType currentWeapon = WeaponType.Barefist; // default starting weapon

    public int GetDamage()
    {
        switch (currentWeapon)
        {
            case WeaponType.Barefist: return 2;
            case WeaponType.Rustyknife: return 4;
            case WeaponType.Sickle: return 8;
            case WeaponType.Scythe: return 6;
            case WeaponType.Machete: return 10;
            default: return 1;
        }
    }
}
