using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public List<Quest> questList = new List<Quest>();
    [SerializeField]
    public List<QuestContent> playerQuestList = new List<QuestContent>();
    public List<QuestContent> playerCompleteQuestList = new List<QuestContent>();

    private Player player;

    [SerializeField]
    private GameObject questUI;
    [SerializeField]
    private Transform questContent;

    public UnityEvent onValueChanged = new UnityEvent();


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        questList = DataManager.GetQuestDB();
        
        LoadQuestData();
        LoadPlayerQuest();

        player.onKillMonster.AddListener(OnValueChanged);
        InventoryManager.instance.onResourcesChanged.AddListener(OnValueChanged);
    }

    public List<Quest> FindQuestList(string npcID)
    {
        List<Quest> ql = new List<Quest>();

        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].npc == npcID)
            {
                ql.Add(questList[i]);
            }
        }

        return ql;
    }

    public QuestState GetQuestState(string questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].name == questID)
            {
                return questList[i].state;
            }
        }

        return QuestState.NULL;
    }

    public void AddPlayerQuest(Quest quest)
    {
        QuestContent qc = new QuestContent();

        qc.quest = quest;
        qc.currentAmount = 0;
        SetQuestReward(qc);

        playerQuestList.Add(qc);

        UIQuest uiQuest = Instantiate(questUI, questContent).GetComponent<UIQuest>();
        uiQuest.Setup(qc);

        SavePlayerQuest();
    }

    public void SetQuestReward(QuestContent qc)
    {
        for (int i = 0; i < qc.quest.rewards.Count; i++)
        {
            if (qc.quest.rewards[i].reward.Equals("exp"))
            {
                qc.exp = qc.quest.rewards[i].amount;
            }
            else if (qc.quest.rewards[i].reward.Equals("gold"))
            {
                qc.gold = qc.quest.rewards[i].amount;
            }
            else if (qc.quest.rewards[i].reward.Equals("weapon") || qc.quest.rewards[i].reward.Equals("armor"))
            {
                qc.item = new Item[qc.quest.rewards[i].amount];
                for (int j = 0; j < qc.quest.rewards[i].amount; j++)
                {
                    qc.item[j] = ItemGenerator.instance.GenerateItem(qc.quest.rewards[i].reward, qc.quest.rewards[i].rarity);
                }
            }
        }
    }

    public void RecieveRewards(QuestContent qc)
    {
        qc.quest.state = QuestState.Complete;
        playerCompleteQuestList.Add(qc);
        playerQuestList.Remove(qc);
        SaveQuestData();

        player.status.exp += qc.exp;
        InventoryManager.instance.AddGold(qc.gold);
        for (int i = 0; i < qc.item.Length; i++)
        {
            InventoryManager.instance.AddItem(qc.item[i]);
        }
    }

    private void OnValueChanged(string name, int value)
    {
        for (int i = 0; i < playerQuestList.Count; i++)
        {
            if (playerQuestList[i].quest.type == name)
            {
                playerQuestList[i].currentAmount += value;
                onValueChanged.Invoke();

                Debug.Log($"{playerQuestList[i].quest.name} : {playerQuestList[i].currentAmount}/{playerQuestList[i].quest.amount}");

                SavePlayerQuest();
            }
        }
    }

    [System.Serializable]
    public class PlayerQuestSaveData
    {
        public List<PlayerQuestData> quests = new List<PlayerQuestData>();
        public List<PlayerQuestData> completes = new List<PlayerQuestData>();
    }

    private void SavePlayerQuest()
    {
        if (SaveDataManager.instance.saveQuest == false) return;

        PlayerQuestSaveData saveData = new PlayerQuestSaveData();

        for (int i = 0; i < playerQuestList.Count; i++)
        {
            PlayerQuestData data = new PlayerQuestData();
            data.content = playerQuestList[i];
            
            for (int j = 0; j < playerQuestList[i].item.Length; j++)
            {
                ItemSaveData itemData = new ItemSaveData(playerQuestList[i].item[j]);
                data.items.Add(itemData);
            }

            saveData.quests.Add(data);
        }

        for (int i = 0; i < playerCompleteQuestList.Count; i++)
        {
            PlayerQuestData data = new PlayerQuestData();
            data.content = playerCompleteQuestList[i];

            for (int j = 0; j < playerCompleteQuestList[i].item.Length; j++)
            {
                ItemSaveData itemData = new ItemSaveData(playerCompleteQuestList[i].item[j]);
                data.items.Add(itemData);
            }

            saveData.completes.Add(data);
        }

        JsonIO.SaveToJson(saveData, SaveDataManager.saveFile[SaveFile.PlayerQuest]);
    }

    private void LoadPlayerQuest()
    {
        if (SaveDataManager.instance.loadQuest == false) return;

        PlayerQuestSaveData saveData = JsonIO.LoadFromJson<PlayerQuestSaveData>(SaveDataManager.saveFile[SaveFile.PlayerQuest]);
        if (saveData != null)
        {
            for (int i = 0; i < saveData.quests.Count; i++)
            {
                saveData.quests[i].content.item = new Item[saveData.quests[i].items.Count];
                for (int j = 0; j < saveData.quests[i].items.Count; j++)
                {
                    Item item = saveData.quests[i].items[j].DeepCopy();
                    saveData.quests[i].content.item[j] = item;
                }
                playerQuestList.Add(saveData.quests[i].content);
            }

            for (int i = 0; i < saveData.completes.Count; i++)
            {
                saveData.completes[i].content.item = new Item[saveData.completes[i].items.Count];
                for (int j = 0; j < saveData.completes[i].items.Count; j++)
                {
                    Item item = saveData.completes[i].items[j].DeepCopy();
                    saveData.completes[i].content.item[j] = item;
                }
                playerCompleteQuestList.Add(saveData.completes[i].content);
            }

            for (int i = 0; i < playerQuestList.Count; i++)
            {
                UIQuest uiQuest = Instantiate(questUI, questContent).GetComponent<UIQuest>();
                uiQuest.Setup(playerQuestList[i]);
            }

            for (int i = 0; i < playerCompleteQuestList.Count; i++)
            {
                UIQuest uiQuest = Instantiate(questUI, questContent).GetComponent<UIQuest>();
                uiQuest.Setup(playerCompleteQuestList[i]);
            }
        }
    }

    [System.Serializable]
    public class QuestListSaveData
    {
        public List<QuestListData> datas = new List<QuestListData>(); 
    }

    public void SaveQuestData()
    {
        if (SaveDataManager.instance.saveQuest == false) return;

        QuestListSaveData saveData = new QuestListSaveData();
        
        for (int i = 0; i < questList.Count; i++)
        {
            QuestListData data = new QuestListData();
            data.questID = questList[i].name;
            data.questState = questList[i].state;
            saveData.datas.Add(data);
        }

        JsonIO.SaveToJson(saveData, SaveDataManager.saveFile[SaveFile.QuestData]);
    }

    private void LoadQuestData()
    {
        if (SaveDataManager.instance.loadQuest == false) return;

        QuestListSaveData saveData = JsonIO.LoadFromJson<QuestListSaveData>(SaveDataManager.saveFile[SaveFile.QuestData]);

        if (saveData != null)
        {
            for (int i = 0; i < saveData.datas.Count; i++)
            {
                for (int j = 0; j < questList.Count; j++)
                {
                    if (questList[j].name == saveData.datas[i].questID)
                    {
                        questList[j].state = saveData.datas[i].questState;
                        break;
                    }
                }
            }
        }
    }
}

[System.Serializable]
public class QuestContent
{
    public Quest quest;
    public int currentAmount;
    public Item[] item = null;
    public int gold = 0;
    public int exp = 0;
}

public enum QuestState { NULL, NotProgress, Progress, Complete }

[System.Serializable]
public class Quest
{
    public string name;
    public string type;
    public int amount;
    public List<QuestReward> rewards = new List<QuestReward>();
    public string npc;
    public int minlvl;
    public int maxlvl;
    public int chance;
    public string title;
    public string content;
    public QuestState state = QuestState.NotProgress;
}

[System.Serializable]
public class QuestReward
{
    public string reward;
    public string rarity;
    public int amount;
}

[System.Serializable]
public class PlayerQuestData
{
    public QuestContent content;
    public List<ItemSaveData> items = new List<ItemSaveData>();
}

[System.Serializable]
public class QuestListData
{
    public string questID;
    public QuestState questState;
}
