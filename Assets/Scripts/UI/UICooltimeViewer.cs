using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICooltimeViewer : MonoBehaviour
{
    private Shortcut shortcut;
    private PlayerSkill playerSkill;
    private Image cooltimeImage;

    private void Awake()
    {
        shortcut = GetComponent<Shortcut>();
        playerSkill = FindObjectOfType<PlayerSkill>();
        cooltimeImage = transform.Find("RedCover").GetComponent<Image>();
    }

    private void Update()
    {
        if (shortcut.GetSkill() == null || playerSkill.isSkillCool.ContainsKey(shortcut.GetSkill()) == false)
        {
            cooltimeImage.fillAmount = 0;
            return;
        }
        else if (playerSkill.isSkillCool[shortcut.GetSkill()])
        {
            cooltimeImage.fillAmount = 1 - playerSkill.skillCool[shortcut.GetSkill()].GetTime / shortcut.GetSkill().cooltime;
        }
        else cooltimeImage.fillAmount = 0;
    }
}
