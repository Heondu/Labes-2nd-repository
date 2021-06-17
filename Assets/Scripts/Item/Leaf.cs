using UnityEngine;

public class Leaf : DropItem, IItem
{
    private int amount = 0;

    public void Use()
    {
        InventoryManager.instance.AddLeaf(amount);

        if (effect)
            Instantiate(effect, transform.position, Quaternion.identity);

        if (sound)
            SoundEffectManager.SoundEffect(sound);

        gameObject.SetActive(false);
    }

    public void SetAmount(int value)
    {
        amount = value;
    }
}
