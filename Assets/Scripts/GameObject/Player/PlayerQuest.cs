using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    [SerializeField]
    private List<QuestContent> questList = new List<QuestContent>();

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
        player.onKillMonster.AddListener(OnKillMonster);
    }

    public void AddQuest(Quest quest)
    {
        QuestContent qc = new QuestContent();

        qc.quest = quest;
        qc.currentAmount = 0;

        questList.Add(qc);
    }

    private void OnKillMonster(string name)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].quest.type == name)
            {
                questList[i].currentAmount++;

                Debug.Log($"{questList[i].quest.name} : {questList[i].currentAmount}/{questList[i].quest.amount}");

                if (questList[i].quest.amount <= questList[i].currentAmount)
                {
                    Debug.Log($"{questList[i].quest.name} -COMPLETE-");
                    questList[i].quest.state = QuestState.Complete;
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
}
