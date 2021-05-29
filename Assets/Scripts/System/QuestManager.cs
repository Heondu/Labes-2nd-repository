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
    private GameObject questCompleteUI;
    [SerializeField]
    private Transform questContent;

    public UnityEvent onValueChanged = new UnityEvent();
    public UnityEvent onQuestCompleted = new UnityEvent();


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        questList = DataManager.GetQuestDB();
        player.onKillMonster.AddListener(OnValueChanged);
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
        player.status.exp += qc.exp;
        InventoryManager.instance.AddGold(qc.gold);
        for (int i = 0; i < qc.item.Length; i++)
        {
            InventoryManager.instance.AddItem(qc.item[i]);
        }
    }

    private void OnValueChanged(string name)
    {
        for (int i = 0; i < playerQuestList.Count; i++)
        {
            if (playerQuestList[i].quest.type == name)
            {
                playerQuestList[i].currentAmount++;
                onValueChanged.Invoke();

                Debug.Log($"{playerQuestList[i].quest.name} : {playerQuestList[i].currentAmount}/{playerQuestList[i].quest.amount}");

                if (playerQuestList[i].quest.amount <= playerQuestList[i].currentAmount)
                {
                    Debug.Log($"{playerQuestList[i].quest.name} -COMPLETE-");
                    playerQuestList[i].quest.state = QuestState.Complete;
                    playerCompleteQuestList.Add(playerQuestList[i]);
                    playerQuestList.RemoveAt(i);
                    onQuestCompleted.Invoke();
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
    public QuestState state = QuestState.NotProgress;
}

[System.Serializable]
public class QuestReward
{
    public string reward;
    public string rarity;
    public int amount;
}
