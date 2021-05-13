using System.Collections.Generic;
using UnityEngine;

public class InventorySkill : Inventory
{
    public List<Skill> skills = new List<Skill>();
    public Slot[] slots;

    private void Awake()
    {
        slots = GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
            skills.Add(null);
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
        for (int i = 0; i < slots.Length; i++)
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

        if (InventoryManager.instance.onSlotChangedCallback != null)
        {
            InventoryManager.instance.onSlotChangedCallback.Invoke();
        }
    }
}
