using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeDialogue : MonoBehaviour
{
    [SerializeField]
    private string dialogue1;
    [SerializeField]
    private float dialogueDelay = 0.1f;

    private void Awake()
    {
        TypeWriter.Write(GetComponent<TextMeshProUGUI>(), dialogue1, dialogueDelay);
    }
}
