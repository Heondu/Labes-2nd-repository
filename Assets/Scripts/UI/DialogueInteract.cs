using UnityEngine;

public class DialogueInteract : MonoBehaviour, IInteractive
{
    [SerializeField]
    private GameObject dialogueL;
    [SerializeField]
    private GameObject dialogueR;
    private TxtBubble bubbleL;
    private TxtBubble bubbleR;

    private void Awake()
    {
        bubbleL = dialogueL.GetComponent<TxtBubble>();
        bubbleR = dialogueR.GetComponent<TxtBubble>();
    }

    public void Execute(NPC npc)
    {
        bubbleL.Init(npc.GetDialogue());
    }
}

public class Dialogue
{
    public Sprite face;
    public string content;
    public float interval;
}
