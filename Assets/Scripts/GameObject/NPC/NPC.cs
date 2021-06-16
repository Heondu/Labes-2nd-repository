using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private string nameID;
    [SerializeField]
    private string[] codes;

    [SerializeField]
    private List<Quest> questList = new List<Quest>();
    [SerializeField]
    private List<Quest> newQuests = new List<Quest>();

    private Player player;
    private PlayerItem playerItem;
    private Animator anim;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerItem = player.GetComponent<PlayerItem>();
        player.onLevelUp.AddListener(QuestEventActive);
        anim = GetComponent<Animator>();

        questList = QuestManager.instance.FindQuestList(nameID);

        StartCoroutine(NpcAnimation());

        QuestEventActive();
    }

    IEnumerator NpcAnimation() //아이들 애니메이션 스위칭
    {
        if(Random.Range(1,10) < 5)
        {
            anim.SetTrigger("Special");
        }
        yield return new WaitForSeconds(1);
    }

    public void ShowDialogue()
    {
        string code = codes[Random.Range(0, codes.Length)];
        DialogueManager.instance.StartDialogue(transform, code);
    }

    public void Quest()
    {
        //for (int i = 0; i < QuestManager.instance.playerQuestList.Count; i++)
        //{
        //    if (QuestManager.instance.playerQuestList[i].quest.state == QuestState.Complete)
        //    {
        //        Debug.Log($"{QuestManager.instance.playerQuestList[i].quest.name} CPMPLETE!!");
        //        QuestManager.instance.playerQuestList.RemoveAt(i);
        //        PlayerInput.instance.SetInputMode(InputMode.normal);
        //        return;
        //    }
        //}

        if (newQuests.Count > 0)
        {
            Debug.Log($"{newQuests[0].name}");
            newQuests[0].state = QuestState.Progress;
            QuestManager.instance.AddPlayerQuest(newQuests[0]);
            QuestManager.instance.SaveQuestData();
            newQuests.RemoveAt(0);

            PlayerInput.SetInputMode(InputMode.normal);
        }
    }

    private void QuestEventActive()
    {
        newQuests.Clear();

        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].minlvl <= player.status.level && questList[i].maxlvl >= player.status.level)
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

        newQuests.Sort(delegate (Quest a, Quest b)
        {
            if (a.minlvl < b.minlvl)
            {
                return -1;
            }
            else if (a.minlvl > b.minlvl)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        });
    }
}
