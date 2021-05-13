using System.Collections.Generic;
using UnityEngine;

public enum UseType { weapon = 0, equipment, skill, consume, rune, equipSlot }

public class InventoryItemData
{
    public List<ItemSaveData> items = new List<ItemSaveData>();
}

public class InventorySkillData
{
    public List<SkillSaveData> skills = new List<SkillSaveData>();
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public InventoryItem inventoryWeapon;
    public InventoryItem inventoryEquipment;
    public InventorySkill inventorySkill;
    public InventoryItem inventoryConsume;
    public InventoryItem equipSlotL;
    public InventoryItem equipSlotR;
    public InventorySkill skillSlot;
    public InventoryItem consumeSlot;

    public delegate void OnSlotChanged();
    public OnSlotChanged onSlotChangedCallback;
    public delegate void OnItemEquipChanged(Item item);
    public OnItemEquipChanged onItemEquipCallback;
    public delegate void OnItemUnequipChanged(Item item);
    public OnItemUnequipChanged onItemUnequipCallback;

    private SlotDrag draggingSlot;
    [SerializeField]
    private GameObject draggingObject;

    public Notification notification;

    public bool itemSave = false;
    public bool itemLoad = false;
    public bool skillSave = false;
    public bool skillLoad = false;
    public bool resourceSave = false;
    public bool resourceLoad = false;

    private Resource resource = new Resource();

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        draggingSlot = draggingObject.GetComponent<SlotDrag>();
    }

    private void Start()
    {
        LoadInventory();
        LoadResource();
    }

    private void SaveInventory()
    {
        if (itemSave)
        {
            inventoryWeapon.SaveInventory();
            inventoryEquipment.SaveInventory();
            inventoryConsume.SaveInventory();
            equipSlotL.SaveInventory();
            equipSlotR.SaveInventory();
            consumeSlot.SaveInventory();
        }
        if (skillSave)
        {
            inventorySkill.SaveInventory();
            skillSlot.SaveInventory();
        }
    }

    private void LoadInventory()
    {
        if (itemLoad)
        {
            inventoryWeapon.LoadInventory();
            inventoryEquipment.LoadInventory();
            inventoryConsume.LoadInventory();
            equipSlotL.LoadInventory();
            equipSlotR.LoadInventory();
            consumeSlot.LoadInventory();
        }
        if (skillSave)
        {
            inventorySkill.LoadInventory();
            skillSlot.LoadInventory();
        }
    }

    private void SaveResource()
    {
        if (resourceSave)
        {
            JsonIO.SaveToJson(resource, SaveDataManager.saveFile[SaveFile.PlayerResources]);
        }
    }

    private void LoadResource()
    {
        if (resourceLoad)
        {
            resource = JsonIO.LoadFromJson<Resource>(SaveDataManager.saveFile[SaveFile.PlayerResources]);
        }
    }

    public void AddItem(Item newItem)
    {
        if (newItem.useType == "weapon") inventoryWeapon.AddItem(newItem);
        else if (newItem.useType == "equipment") inventoryEquipment.AddItem(newItem);
        else if (newItem.useType == "consume") inventoryConsume.AddItem(newItem);
        notification.Notify(true);
        notification.IncreaseNum();
    }
    
    public void AddSkill(Skill newSkill)
    {
        if (skillLoad == false)
        {
            inventorySkill.AddSkill(newSkill);
            notification.Notify(true);
            notification.IncreaseNum();
        }
    }

    public void RemoveItem(Item selectedItem)
    {
        if (selectedItem.useType == "weapon") inventoryWeapon.RemoveItem(selectedItem);
        else if (selectedItem.useType == "equipment") inventoryEquipment.RemoveItem(selectedItem);
        else if (selectedItem.useType == "consume") inventoryConsume.RemoveItem(selectedItem);
    }

    private void UpdateInventory(Slot slot)
    {
        slot.inventory.UpdateInventory();

        SaveInventory();
    }

    public void OnBeginDrag(Slot selectedSlot)
    {
        draggingObject.SetActive(true);
        draggingSlot.icon.sprite = selectedSlot.icon.sprite;
        if (selectedSlot.item != null) draggingSlot.qualty.text = selectedSlot.item.quality == 0 ? "" : selectedSlot.item.quality + "+";
        else if (selectedSlot.skill != null) draggingSlot.qualty.text = selectedSlot.skill.quality == 0 ? "" : selectedSlot.skill.quality + "+";
        selectedSlot.icon.color = Color.clear;
    }

    public void OnDrag()
    {
        draggingObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) * Vector2.one;
    }

    public void OnEndDrag(Slot selectedSlot, Slot targetSlot)
    {
        draggingObject.SetActive(false);
        if (targetSlot == null)
        {
            UpdateInventory(selectedSlot);
            return;
        }

        if (targetSlot.isEquipSlot)
        {
            if (selectedSlot.useType == UseType.weapon && targetSlot.equipType == "weapon") Equip(selectedSlot, targetSlot);
            else if (selectedSlot.useType == UseType.equipment && selectedSlot.item.type == targetSlot.equipType) Equip(selectedSlot, targetSlot);
            else if (selectedSlot.useType == UseType.skill && targetSlot.equipType == "skill") Equip(selectedSlot, targetSlot);
            else if (selectedSlot.useType == UseType.consume && targetSlot.equipType == "consume") Equip(selectedSlot, targetSlot);
            else UpdateInventory(selectedSlot);
        }
        else if (selectedSlot.useType == targetSlot.useType)
        {
            if (targetSlot.equipType == "" || selectedSlot.equipType == targetSlot.equipType)
            {
                targetSlot.inventory.ChangeSlot(selectedSlot, targetSlot);
                selectedSlot.inventory.ChangeSlot(targetSlot, selectedSlot);
                UpdateInventory(selectedSlot);
                UpdateInventory(targetSlot);
            }
            else
            {
                UpdateInventory(selectedSlot);
            }
        }
        else
        {
            UpdateInventory(selectedSlot);
        }
    }

    private void Equip(Slot selectedSlot, Slot targetSlot)
    {
        targetSlot.inventory.ChangeSlot(selectedSlot, targetSlot);
        selectedSlot.inventory.ChangeSlot(targetSlot, selectedSlot);
        UpdateInventory(selectedSlot);
        UpdateInventory(targetSlot);
    }

    public void QuickEquip(Slot selectedSlot)
    {
        if (selectedSlot.useType == UseType.weapon)
        {
            if (selectedSlot.isEquipSlot)
            {
                for (int i = 0; i < inventoryWeapon.slots.Length; i++)
                {
                    if (inventoryWeapon.slots[i].item == null)
                    {
                        Equip(selectedSlot, inventoryWeapon.slots[i]);
                        return;
                    }
                }
            }
            else Equip(selectedSlot, equipSlotL.slots[0]);
        }
        else if (selectedSlot.useType == UseType.equipment)
        {
            if (selectedSlot.isEquipSlot)
            {
                for (int i = 0; i < inventoryEquipment.slots.Length; i++)
                {
                    if (inventoryEquipment.slots[i].item == null)
                    {
                        Equip(selectedSlot, inventoryEquipment.slots[i]);
                        return;
                    }
                }
            }
            else
            {
                for (int i = 0; i < equipSlotL.slots.Length; i++)
                {
                    if (selectedSlot.item.type == equipSlotL.slots[i].equipType)
                    {
                        Equip(selectedSlot, equipSlotL.slots[i]);
                        return;
                    }
                }
                for (int i = 0; i < equipSlotR.slots.Length; i++)
                {
                    if (selectedSlot.item.type == equipSlotR.slots[i].equipType)
                    {
                        Equip(selectedSlot, equipSlotR.slots[i]);
                        return;
                    }
                }
            }
        }
        else if (selectedSlot.useType == UseType.skill)
        {
            if (selectedSlot.isEquipSlot)
            {
                for (int i = 0; i < inventorySkill.slots.Length; i++)
                {
                    if (inventorySkill.slots[i].skill == null)
                    {
                        Equip(selectedSlot, inventorySkill.slots[i]);
                        return;
                    }
                }
            }
            else
            {
                for (int i = 0; i < skillSlot.slots.Length; i++)
                {
                    if (skillSlot.slots[i].skill == null)
                    {
                        Equip(selectedSlot, skillSlot.slots[i]);
                        return;
                    }
                }
            }
        }
        else if (selectedSlot.useType == UseType.consume)
        {
            if (selectedSlot.isEquipSlot)
            {
                for (int i = 0; i < inventoryConsume.slots.Length; i++)
                {
                    if (inventoryConsume.slots[i].item == null)
                    {
                        Equip(selectedSlot, inventoryConsume.slots[i]);
                        return;
                    }
                }
            }
            else
            {
                for (int i = 0; i < consumeSlot.slots.Length; i++)
                {
                    if (consumeSlot.slots[i].item == null)
                    {
                        Equip(selectedSlot, consumeSlot.slots[i]);
                        return;
                    }
                }
            }
        }
    }

    public void AddGold(int value)
    {
        resource.gold += value;
        SaveResource();
        
    }
    public void SubGold(int value)
    {
        resource.gold -= value;
        SaveResource();
    }
    public int GetGold()
    {
        return resource.gold;
    }

    public void AddOre(int value)
    {
        resource.ore += value;
        SaveResource();
    }
    public void SubOre(int value)
    {
        resource.ore -= value;
        SaveResource();
    }
    public int GetOre()
    {
        return resource.ore;
    }

    public void AddLeaf(int value)
    {
        resource.leaf += value;
        SaveResource();
    }
    public void SubLeaf(int value)
    {
        resource.leaf -= value;
        SaveResource();
    }
    public int GetLeaf()
    {
        return resource.leaf;
    }
}
