using UnityEngine;
using UnityEngine.UI;

public class SlotShortcut : MonoBehaviour
{
    public Image icon;
    public Skill skill = null;

    private void Awake()
    {
        icon = transform.Find("Image").GetComponent<Image>();
    }

    public void ApplyShortcut(Sprite icon, Skill skill)
    {
        this.icon.sprite = icon;
        this.skill = skill;
        if (skill != null) this.icon.color = Color.white;
        else if (skill == null) this.icon.color = Color.clear;
    }
}
