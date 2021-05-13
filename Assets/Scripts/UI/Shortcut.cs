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
    private KeyAction keycode;

    private void Awake()
    {
        icon = transform.Find("Image").GetComponent<Image>();
        keycodeText = transform.Find("TextKeycode").GetComponent<TextMeshProUGUI>();
        keycodeText.text = KeySetting.keys[keycode].ToString();
    }
    
    private void Update()
    {
        ShortcutAssign();
    }

    private void ShortcutAssign()
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
