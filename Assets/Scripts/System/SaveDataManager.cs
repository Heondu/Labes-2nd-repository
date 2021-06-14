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

        saveFile[SaveFile.PlayerStatus] = "Player Status";
        saveFile[SaveFile.KeyPreset] = "Key Preset";
        saveFile[SaveFile.InventoryWeapon] = "Inventory Weapon";
        saveFile[SaveFile.InventoryEquipment] = "Inventory Equipment";
        saveFile[SaveFile.InventoryEquipSlotL] = "Inventory Left EquipSlot";
        saveFile[SaveFile.InventoryEquipSlotR] = "Inventory Right EquipSlot";
        saveFile[SaveFile.InventorySkill] = "Inventory Skill";
        saveFile[SaveFile.InventorySkillSlot] = "Inventory SkillSlot";
        saveFile[SaveFile.InventoryConsume] = "Inventory Consume";
        saveFile[SaveFile.InventoryConsumeSlot] = "Inventory ConsumeSlot";
        saveFile[SaveFile.InventoryRune] = "Inventory Rune";
        saveFile[SaveFile.PlayerResources] = "Player Resources";
        saveFile[SaveFile.AwakenData] = "Awaken Data";
        saveFile[SaveFile.PlayerQuest] = "Player Quest";
        saveFile[SaveFile.QuestData] = "Quest Data";

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
