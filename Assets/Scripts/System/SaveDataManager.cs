using System.Collections.Generic;
using UnityEngine;

public enum SaveFile
{
    PlayerStatus,
    KeyPreset,
    InventoryWeapon,
    InventoryEquipment,
    InventoryEquipSlotL,
    InventoryEquipSlotR,
    InventorySkill,
    InventorySkillSlot,
    InventoryConsume,
    InventoryConsumeSlot,
    InventoryRune,
    PlayerResources
}

public class SaveDataManager : MonoBehaviour
{
    public static Dictionary<SaveFile, string> saveFile = new Dictionary<SaveFile, string>();

    public void Awake()
    {
        string path = "SaveData/";

        saveFile.Add(SaveFile.PlayerStatus, path + "Player Status");
        saveFile.Add(SaveFile.KeyPreset, path + "Key Preset");
        saveFile.Add(SaveFile.InventoryWeapon, path + "Inventory Weapon");
        saveFile.Add(SaveFile.InventoryEquipment, path + "Inventory Equipment");
        saveFile.Add(SaveFile.InventoryEquipSlotL, path + "Inventory Left EquipSlot");
        saveFile.Add(SaveFile.InventoryEquipSlotR, path + "Inventory Right EquipSlot");
        saveFile.Add(SaveFile.InventorySkill, path + "Inventory Skill");
        saveFile.Add(SaveFile.InventorySkillSlot, path + "Inventory SkillSlot");
        saveFile.Add(SaveFile.InventoryConsume, path + "Inventory Consume");
        saveFile.Add(SaveFile.InventoryConsumeSlot, path + "Inventory ConsumeSlot");
        saveFile.Add(SaveFile.InventoryRune, path + "Inventory Rune");
        saveFile.Add(SaveFile.PlayerResources, path + "Player Resources");
    }
}
