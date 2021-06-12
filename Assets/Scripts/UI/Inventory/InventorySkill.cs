using System.Collections.Generic;
using UnityEngine;

public class InventorySkill : Inventory
{
    public List<Skill> skills = new List<Skill>();
    public List<Slot> slots = new List<Slot>();

    private GameObject slot;

    private void Awake()
    {
        Slot[] slots = GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            this.slots.Add(slots[i]);
            skills.Add(null);
        }

        slot = slots[0].gameObject;
    }

    private void ExpandSlot()
    {
        for (int i = 0; i < 6; i++)
        {
            slots.Add(Instantiate(slot, transform).GetComponent<Slot>());
            skills.Add(null);
        }
    }

    public void SaveInventory()
    {
        InventorySkillData inventoryData = new InventorySkillData();

        for (int i = 0; i < skills.Count; i++)
        {
            inventoryData.skills.Add(new SkillSaveData(skills[i]));
        }

        JsonIO.SaveToJson(inventoryData, SaveDataManager.saveFile[saveFileName]);
    }

    public void LoadInventory()
    {
        InventorySkillData inventoryData = JsonIO.LoadFromJson<InventorySkillData>(SaveDataManager.saveFile[saveFileName]);

        if (inventoryData == null) return;

        for (int i = 0; i < inventoryData.skills.Count; i++)
        {
            if (inventoryData.skills[i].name == "")
            {
                skills[i] = null;
            }
            else
            {
                skills[i] = inventoryData.skills[i].DeepCopy();
            }
        }

        UpdateInventory();
    }

    public void AddSkill(Skill newSkill)
    {
        int index = FindSlot(null);
        if (index == -1)
        {
            ExpandSlot();
            index = FindSlot(null);
        }
        if (index != -1) skills[index] = newSkill;
        OnNotify();
        UpdateInventory();
    }

    public override void ChangeSlot(Slot selectedSlot, Slot targetSlot)
    {
        skills[targetSlot.index] = selectedSlot.skill;
    }

    public int FindSlot(Skill targetSkill)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].skill == targetSkill) return i;
        }

        return -1;
    }

    public override void UpdateInventory()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            slots[i].skill = skills[i];
        }

        SaveInventory();

        if (InventoryManager.instance.onSlotChanged != null)
        {
            InventoryManager.instance.onSlotChanged.Invoke();
        }
    }
}
