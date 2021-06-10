using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        InventoryManager.instance.onItemEquip.AddListener(Equip);
        InventoryManager.instance.onItemUnequip.AddListener(Unequip);
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player.GetStatus("itemRange").Value);

        foreach (Collider2D collider in colliders)
        {
            IItem item = collider.GetComponent<IItem>();

            if (item != null)
            {
                item.MoveToPlayer(transform);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IItem item = collision.GetComponent<IItem>();
        if (item != null)
        {
            item.Use();
        }
    }

    public void Equip(Item item)
    {
        Status status = player.status.GetStatus(item.status);
        if (item.status.Contains("%")) status.AddModifier(new StatusModifier(item.stat, StatusModType.PercentAdd, item));
        else status.AddModifier(new StatusModifier(item.stat, StatusModType.Flat, item));
        for (int i = 0; i < item.statusAdd.Length; i++)
        {
            status = player.status.GetStatus(item.statusAdd[i]);
            if (item.statusAdd[i].Contains("%")) status.AddModifier(new StatusModifier(item.statAdd[i], StatusModType.PercentAdd, item));
            else status.AddModifier(new StatusModifier(item.statAdd[i], StatusModType.Flat, item));
        }
    }

    public void Unequip(Item item)
    {
        Status status = player.status.GetStatus(item.status);
        status.RemoveAllModifiersFromSource(item);
        for (int i = 0; i < item.statusAdd.Length; i++)
        {
            status = player.status.GetStatus(item.statusAdd[i]);
            status.RemoveAllModifiersFromSource(item);
        }
    }
}
