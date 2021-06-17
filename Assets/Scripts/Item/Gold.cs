using UnityEngine;

public class Gold : DropItem, IItem
{
    private int gold = 0;

    public void Use()
    {
        InventoryManager.instance.AddGold(gold);

        if (effect)
            Instantiate(effect, transform.position, Quaternion.identity);

        if (sound)
            SoundEffectManager.SoundEffect(sound);

        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    public void SetGold(int value)
    {
        gold = value;
    }
}
