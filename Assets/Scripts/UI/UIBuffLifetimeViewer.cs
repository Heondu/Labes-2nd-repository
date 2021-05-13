using UnityEngine;
using UnityEngine.UI;

public class UIBuffLifetimeViewer : MonoBehaviour
{
    private Image icon;
    private Image cooltimeImage;
    private SkillLifetime skillLifetime;

    private void Awake()
    {
        icon = GetComponent<Image>();
        cooltimeImage = transform.Find("Cooltime").GetComponent<Image>();
    }

    public void Init(Skill skill, SkillLifetime skillLifetime)
    {
        this.skillLifetime = skillLifetime;
        skillLifetime.onDestroy.AddListener(Destroy);
        icon.sprite = Resources.Load<Sprite>("icons/skill/" + skill.name);
    }

    private void Update()
    {
        cooltimeImage.fillAmount = skillLifetime.GetCurrentTime / skillLifetime.GetLifetime;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
