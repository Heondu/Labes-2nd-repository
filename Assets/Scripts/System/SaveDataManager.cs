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

        saveFile[SaveFile.PlayerStatus] = path + "Player Status";
        saveFile[SaveFile.KeyPreset] = path + "Key Preset";
        saveFile[SaveFile.InventoryWeapon] = path + "Inventory Weapon";
        saveFile[SaveFile.InventoryEquipment] = path + "Inventory Equipment";
        saveFile[SaveFile.InventoryEquipSlotL] = path + "Inventory Left EquipSlot";
        saveFile[SaveFile.InventoryEquipSlotR] = path + "Inventory Right EquipSlot";
        saveFile[SaveFile.InventorySkill] = path + "Inventory Skill";
        saveFile[SaveFile.InventorySkillSlot] = path + "Inventory SkillSlot";
        saveFile[SaveFile.InventoryConsume] = path + "Inventory Consume";
        saveFile[SaveFile.InventoryConsumeSlot] = path + "Inventory ConsumeSlot";
        saveFile[SaveFile.InventoryRune] = path + "Inventory Rune";
        saveFile[SaveFile.PlayerResources] = path + "Player Resources";
        saveFile[SaveFile.AwakenData] = path + "Awaken Data";
        saveFile[SaveFile.PlayerQuest] = path + "Player Quest";
        saveFile[SaveFile.QuestData] = path + "Quest Data";

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
