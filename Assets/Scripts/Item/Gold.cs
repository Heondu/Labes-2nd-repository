using UnityEngine;

public class Gold : DropItem, IItem
{
    private int gold = 0;
    [SerializeField]
    private GameObject GoldEffect;
    [SerializeField]
    private AudioClip GoldSound;


    public void Use()
    {
        InventoryManager.instance.AddGold(gold);

        if (GoldEffect)
            Instantiate(GoldEffect, transform.position, Quaternion.identity);

        if(GoldSound)
            SoundEffectManager.SoundEffect(GoldSound);

        Destroy(gameObject);
    }

    public void SetGold(int value)
    {
        gold = value;
    }
}
