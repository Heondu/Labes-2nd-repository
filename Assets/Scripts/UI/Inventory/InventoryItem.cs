using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : Inventory
{
    public List<Item> items = new List<Item>();
    public List<Slot> slots = new List<Slot>();

    private GameObject slot;

    private void Awake()
    {
        Slot[] slots = GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            this.slots.Add(slots[i]);
            items.Add(null);
        }
        slot = slots[0].gameObject;
    }

    private void ExpandSlot()
    {
        for (int i = 0; i < 6; i++)
        {
            slots.Add(Instantiate(slot, transform).GetComponent<Slot>());
            items.Add(null);
        }
    }

    public void SaveInventory()
    {
        InventoryItemData inventoryData = new InventoryItemData();

        for (int i = 0; i < items.Count; i++)
        {
            inventoryData.items.Add(new ItemSaveData(items[i]));
        }

        JsonIO.SaveToJson(inventoryData, SaveDataManager.saveFile[saveFileName]);
    }

    public void LoadInventory()
    {
        InventoryItemData inventoryData = JsonIO.LoadFromJson<InventoryItemData>(SaveDataManager.saveFile[saveFileName]);

        if (inventoryData == null) return;

        for (int i = 0; i < inventoryData.items.Count; i++)
        {
            if (inventoryData.items[i].name == "")
            {
                items[i] = null;
            }
            else
            {
                items[i] = inventoryData.items[i].DeepCopy();
            }
        }

        UpdateInventory();
    }

    public void AddItem(Item newItem)
    {
        int index = FindSlot(null);
        if (index == -1)
        {
            ExpandSlot();
            index = FindSlot(null);
        }
        if (index != -1) items[index] = newItem;
        OnNotify();
        UpdateInventory();
    }

    public override void ChangeSlot(Slot selectedSlot, Slot targetSlot)
    {
        items[targetSlot.index] = selectedSlot.item;
    }

    public void RemoveItem(Item targetItem)
    {
        int index = FindSlot(targetItem);
        if (index != -1) items[index] = null;
        UpdateInventory();
    }

    public int FindSlot(Item targetItem)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == targetItem) return i;
        }

        return -1;
    }

    public override void UpdateInventory()
    {
        for (int i = 0; i < items.Count; i++)
        {
            slots[i].item = items[i];
        }

        SaveInventory();

        if (InventoryManager.instance.onSlotChanged != null)
        {
            InventoryManager.instance.onSlotChanged.Invoke();
        }
    }
}
