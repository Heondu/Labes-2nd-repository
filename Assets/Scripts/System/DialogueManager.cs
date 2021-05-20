using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private Dictionary<string, DialogueCollection> dialogueDB = new Dictionary<string, DialogueCollection>();

    [SerializeField]
    private GameObject dialogueL;
    [SerializeField]
    private GameObject dialogueR;
    private TxtBubble bubbleL;
    private TxtBubble bubbleR;
    [SerializeField]
    private float dialogueOffsetY = 2;

    private Transform player;
    private Transform npc;
    private int line = 0;
    private string code = "";

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        bubbleL = dialogueL.GetComponent<TxtBubble>();
        bubbleR = dialogueR.GetComponent<TxtBubble>();
        player = FindObjectOfType<Player>().transform;
    }

    private void Start()
    {
        dialogueDB = DataManager.GetDialogueDB();
        bubbleL.SetCallback(PrintDialogueCallBack);
        bubbleR.SetCallback(PrintDialogueCallBack);
    }

    private TxtBubble SetDialogue()
    {
        Transform target;

        if (dialogueDB[code].dialogues[line].name != "player") target = npc;
        else target = player;

        if (target.position.x > Camera.main.transform.position.x)
        {
            if (dialogueL.activeSelf == false)
            {
                if (dialogueR.activeSelf) bubbleR.SetDieTrigger();
                bubbleL.Init();
                return bubbleL;
            }
            else return bubbleL;
        }
        else
        {
            if (dialogueR.activeSelf == false)
            {
                if (dialogueL.activeSelf) bubbleL.SetDieTrigger();
                bubbleR.Init();
                return bubbleR;
            }
            else return bubbleR;
        }
    }

    private void UpdateDialoguePos(Transform target)
    {
        if (dialogueL.activeSelf)
            dialogueL.transform.position = new Vector3(target.position.x, target.position.y + dialogueOffsetY, 0);
        if (dialogueR.activeSelf)
            dialogueR.transform.position = new Vector3(target.position.x, target.position.y + dialogueOffsetY, 0);
    }

    public void StartDialogue(Transform npc, string code)
    {
        Debug.Assert(dialogueDB.ContainsKey(code));

        StopCoroutine("DialogueCo");
        this.npc = npc;
        line = 0;
        this.code = code;
        StartCoroutine("DialogueCo");
        PrintDialogue();
    }

    private IEnumerator DialogueCo()
    {
        while  (line < dialogueDB[code].dialogues.Count)
        {
            if (dialogueDB[code].dialogues[line].name != "player")
                UpdateDialoguePos(npc);
            else  UpdateDialoguePos(player);

            yield return null;
        }
    }

    private void PrintDialogue()
    {
        if (line >= dialogueDB[code].dialogues.Count)
        {
            dialogueDB[code].isRead = true;

            if (dialogueL.activeSelf)
            {
                bubbleL.SetDieTrigger();
            }
            else if (dialogueR.activeSelf)
            {
                bubbleR.SetDieTrigger();
            }
            PlayerInput.instance.SetInputMode(InputMode.normal);
            return;
        }

        TxtBubble bubble = SetDialogue();

        //if (dialogueDB[code].isRead)
        //{
        //    line = dialogueDB[code].dialogues.Count - 1;
        //}

        bubble.ChangeValue(dialogueDB[code].dialogues[line].face, dialogueDB[code].dialogues[line].content, dialogueDB[code].dialogues[line].interval);
        bubble.WriteText();
    }

    private void PrintDialogueCallBack()
    {
        line++;

        PrintDialogue();
    }
}
