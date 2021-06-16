using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInteract : MonoBehaviour
{
    private NPC npc;

    [SerializeField]
    private float waitTime = 2;
    private float currentTime = 0;
    [SerializeField]
    private float range = 2;
    [SerializeField]
    private float iconRadius = 100;
    [SerializeField]
    private float yOffset = 1;

    private LayerMask layerMask;

    [SerializeField]
    private GameObject speachBubble;
    private Image speachBubbleImage;
    [SerializeField] 
    private GameObject interactiveHolder;
    private GameObject[] interactives;
    [SerializeField]
    private KeyCode[] keyCodes;

    private bool isActiveInteractiveIcon = false;
    private bool isKeyInput = false;

    private void Start()
    {
        speachBubbleImage = speachBubble.GetComponent<Image>();
        npc = GetComponent<NPC>();
        layerMask = 1 << LayerMask.NameToLayer("Player");
        SetInteractives();
    }

    private void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, range, layerMask);

        if (collider != false)
        {
            if (isActiveInteractiveIcon == false && isKeyInput == false)
            {
                speachBubble.SetActive(true);
                speachBubble.transform.position = new Vector3(transform.position.x, transform.position.y + yOffset * 2, 0);

                speachBubbleImage.fillAmount = currentTime / waitTime;

                currentTime += Time.deltaTime;
            }
            else if (isActiveInteractiveIcon == true && isKeyInput == false)
            {
                interactiveHolder.transform.position = new Vector3(transform.position.x, transform.position.y + yOffset, 0);
                interactiveHolder.SetActive(true);
                KeyInputCheck();
            }
        }
        else
        {
            isKeyInput = false;
            DisableInteractive();
        }

        if (currentTime >= waitTime)
        {
            speachBubble.SetActive(false);
            isActiveInteractiveIcon = true;
            PlayerInput.SetInputMode(InputMode.interact);
        }
    }

    private void SetInteractives()
    {
        int count = interactiveHolder.transform.childCount;

        interactives = new GameObject[count];

        float angle = (float)360 / count;
        for (int i = 0; i < count; i++)
        {
            Vector3 newPos = new Vector3(Mathf.Cos((angle * i + 90) * Mathf.Deg2Rad), Mathf.Sin((angle * i + 90) * Mathf.Deg2Rad), 0) * iconRadius;
            interactives[i] = interactiveHolder.transform.GetChild(i).gameObject;
            interactives[i].transform.localPosition = newPos;
            interactives[i].GetComponentInChildren<TextMeshProUGUI>().text = keyCodes[i].ToString();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void KeyInputCheck()
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                if (interactives[i].CompareTag("Dialogue"))
                {
                    npc.ShowDialogue();
                }
                else if (interactives[i].CompareTag("Quest"))
                {
                    npc.Quest();
                }
                else if (interactives[i].CompareTag("Close"))
                {
                    isKeyInput = true;
                    PlayerInput.SetInputMode(InputMode.normal);
                    DisableInteractive();
                }
                else
                {
                    PlayerInput.SetInputMode(InputMode.normal);
                }
                isKeyInput = true;
                DisableInteractive();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isKeyInput = true;
            PlayerInput.SetInputMode(InputMode.normal);
            DisableInteractive();
        }
    }

    private void DisableInteractive()
    {
        currentTime = 0;
        isActiveInteractiveIcon = false;
        speachBubble.SetActive(false);
        interactiveHolder.SetActive(false);
    }
}
