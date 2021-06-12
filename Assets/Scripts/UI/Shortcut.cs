using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shortcut : MonoBehaviour
{
    [SerializeField]
    private Slot slot;
    private Image icon;
    private TextMeshProUGUI keycodeText;
    private Skill skill;
    [SerializeField]
    private KeyAction keyAction;

    [SerializeField]
    private UIKeyChanger keyChanger;

    private void Start()
    {
        icon = transform.Find("Image").GetComponent<Image>();
        keycodeText = transform.Find("TextKeycode").GetComponent<TextMeshProUGUI>();
        UpdateKeyCode();

        keyChanger.onKeyChanged.AddListener(UpdateKeyCode);
    }

    private void UpdateKeyCode()
    {
        keycodeText.text = KeySetting.keys[keyAction].ToString();
    }
    
    private void Update()
    {
        AssignShortcut();
    }

    private void AssignShortcut()
    {
        if (slot == null) return;
        icon.sprite = slot.icon.sprite;
        icon.color = slot.icon.color;
        if (slot.skill != null) skill = slot.skill;
        else if (slot.item != null && slot.item.skill != null) skill = slot.item.skill;
        else skill = null;
    }

    public Skill GetSkill()
    {
        return skill;
    }
}