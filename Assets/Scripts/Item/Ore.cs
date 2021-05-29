using UnityEngine;

public class Ore : DropItem, IItem
{
    private int amount = 0;
    [SerializeField]
    private GameObject effect;
    [SerializeField]
    private AudioClip sound;


    public void Use()
    {
        InventoryManager.instance.AddOre(amount);

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
