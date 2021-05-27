using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public List<Quest> questList = new List<Quest>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        questList = DataManager.GetQuestDB();
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
