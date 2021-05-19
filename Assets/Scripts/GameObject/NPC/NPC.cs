using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private string id;

    [SerializeField]
    private int dialogueIndex = 0;

    [SerializeField]
    private GameObject dialogueL;
    [SerializeField]
    private GameObject dialogueR;
    private TxtBubble bubbleL;
    private TxtBubble bubbleR;
    [SerializeField]
    private float dialogueOffsetY = 2;

    private bool isOnDialogue = false;

    private Player player;

    private void Awake()
    {
        bubbleL = dialogueL.GetComponent<TxtBubble>();
        bubbleR = dialogueR.GetComponent<TxtBubble>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (isOnDialogue && dialogueL.activeSelf)
            dialogueL.transform.position = new Vector3(transform.position.x, transform.position.y + dialogueOffsetY, 0);
        if (isOnDialogue && dialogueR.activeSelf)
            dialogueR.transform.position = new Vector3(transform.position.x, transform.position.y + dialogueOffsetY, 0);
    }

    public void OnDialogue()
    {
        bubbleL.Init(GetDialogue());
        isOnDialogue = true;
    }

    public List<Dialogue> GetDialogue()
    {
        return DataManager.FindDialogue(id, dialogueIndex, out dialogueIndex);
    }

    public void AddDialogueIndex()
    {
        dialogueIndex++;
    }

    public void SetDialogueIndex(int value)
    {
        dialogueIndex = value;
    }
}
