using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private string nameID;
    [SerializeField]
    private string code;

    [SerializeField]
    private List<Quest> questList = new List<Quest>();
    [SerializeField]
    private List<Quest> newQuests = new List<Quest>();
    [SerializeField]
    private List<Quest> currentQuests = new List<Quest>();

    private Player player;
    private PlayerQuest playerQuest;
    private PlayerItem playerItem;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerQuest = player.GetComponent<PlayerQuest>();
        playerItem = player.GetComponent<PlayerItem>();
        player.onLevelUp.AddListener(QuestEventActive);

        questList = QuestManager.instance.FindQuestList(nameID);

        QuestEventActive();
    }

    public void ShowDialogue()
    {
        if (currentQuests.Count > 0)
        {
            for (int i = 0; i < currentQuests.Count; i++)
            {
                if (currentQuests[i].state == QuestState.Complete)
                {
                    Debug.Log($"{currentQuests[i].name}");
                    currentQuests.RemoveAt(i);
                    PlayerInput.instance.SetInputMode(InputMode.normal);
                    return;
                }
            }

        }
        if (newQuests.Count > 0)
        {
            Debug.Log($"{newQuests[0].name} COMPLETE!!");
            newQuests[0].state = QuestState.Progress;
            currentQuests.Add(newQuests[0]);
            playerQuest.AddQuest(newQuests[0]);
            newQuests.RemoveAt(0);

            PlayerInput.instance.SetInputMode(InputMode.normal);
        }
        else
        {
            DialogueManager.instance.StartDialogue(transform, code);
        }
    }

    private void QuestEventActive()
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].minlvl <= player.status.level)
            {
                if (QuestManager.instance.GetQuestState(questList[i].name) == QuestState.NotProgress)
                {
                    int chance = Random.Range(0, 100);

                    if (questList[i].chance >= chance)
                    {
                        newQuests.Add(questList[i]);
                    }
                }
            }
        }
    }
}
