using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotDrag : MonoBehaviour
{
    public Image icon;
    public Image gradeBG;
    public TextMeshProUGUI qualty;

    private void Awake()
    {
        icon = transform.Find("gradeBG").Find("Icon").GetComponent<Image>();
        gradeBG = transform.Find("gradeBG").GetComponent<Image>();
        qualty = gradeBG.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
