using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour, IInteractive
{
    [SerializeField]
    private GameObject dialogueL;
    [SerializeField]
    private GameObject dialogueR;
    private TextMeshProUGUI dialogueLText;
    private TextMeshProUGUI dialogueRText;

    private void Awake()
    {
        dialogueLText = dialogueL.GetComponentInChildren<TextMeshProUGUI>();
        dialogueRText = dialogueL.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Execute()
    {
        dialogueLText.text = "";
        dialogueL.SetActive(true);
    }
}
