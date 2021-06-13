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
    PlayerResources,
    AwakenData,
    PlayerQuest,
    QuestData
}

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance;

    public static Dictionary<SaveFile, string> saveFile = new Dictionary<SaveFile, string>();

    [field: Header("Save")]
    [field: SerializeField] public bool saveStatus { get; private set; }
    [field: SerializeField] public bool saveItem { get; private set; }
    [field: SerializeField] public bool saveSkill { get; private set; }
    [field: SerializeField] public bool saveResource { get; private set; }
    [field: SerializeField] public bool saveKeyPreset { get; private set; }
    [field: SerializeField] public bool saveQuest { get; private set; }
    [field: SerializeField] public bool saveAwaken { get; private set; }

    [field: Header("Load")]
    [field: SerializeField] public bool loadStatus { get; private set; }
    [field: SerializeField] public bool loadItem { get; private set; }
    [field: SerializeField] public bool loadSkill { get; private set; }
    [field: SerializeField] public bool loadResource { get; private set; }
    [field: SerializeField] public bool loadKeyPreset { get; private set; }
    [field: SerializeField] public bool loadQuest { get; private set; }
    [field: SerializeField] public bool loadAwaken { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

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
        saveFile.Add(SaveFile.AwakenData, path + "Awaken Data");
        saveFile.Add(SaveFile.PlayerQuest, path + "Player Quest");
        saveFile.Add(SaveFile.QuestData, path + "Quest Data");

        if (TitleSceneManager.isNewGame)
        {
            NewGame();
        }
        else
        {
            LoadGame();
        }
    }

    public void NewGame()
    {
        loadStatus = false;
        loadItem = false;
        loadSkill = false;
        loadResource = false;
        loadKeyPreset = false;
        loadQuest = false;
        loadAwaken = false;

        saveStatus = true;
        saveItem = true;
        saveSkill = true;
        saveResource = true;
        saveKeyPreset = true;
        saveQuest = true;
        saveAwaken = true;
    }

    public void LoadGame()
    {
        loadStatus = true;
        loadItem = true;
        loadSkill = true;
        loadResource = true;
        loadKeyPreset = true;
        loadQuest = true;
        loadAwaken = true;

        saveStatus = true;
        saveItem = true;
        saveSkill = true;
        saveResource = true;
        saveKeyPreset = true;
        saveQuest = true;
        saveAwaken = true;
    }    
}
