using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private string nameID;
    [SerializeField]
    private string code;

    public void ShowDialogue()
    {
        DialogueManager.instance.StartDialogue(transform, code);
    }
}
