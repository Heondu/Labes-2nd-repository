using UnityEngine;
using System;

public enum WeaponType { sword, axe, gun, bow, fan, staff }

public class WeaponChanger : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer frontWeapon;
    [SerializeField]
    private SpriteRenderer RearWeapon;
    [SerializeField]
    private WeaponType defaultWeapon = WeaponType.bow;
    [SerializeField]
    private Sprite[] weaponSprites;

    private void Start()
    {
        InventoryManager.instance.onItemEquipCallback += GetItem;

        ChangeWeapon((int)defaultWeapon);
    }

    private void GetItem(Item item)
    {
        if (item.useType == "weapon")
        {
            ChangeWeapon((int)Enum.Parse(typeof(WeaponType), item.type));
        }
    }

    private void ChangeWeapon(int index)
    {
        frontWeapon.sprite = weaponSprites[index];
        RearWeapon.sprite = weaponSprites[index];
    }
}
