using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIQuest : MonoBehaviour
{
    private QuestContent questContent;

    [SerializeField]
    private Image background;
    [SerializeField]
    private Sprite completeBackground;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TextMeshProUGUI sliderText;
    [SerializeField]
    private GameObject completeButton;
    [SerializeField]
    private RectTransform rewardHolder;
    [SerializeField]
    private GameObject reward;

    public void Setup(QuestContent qc)
    {
        QuestManager.instance.onValueChanged.AddListener(OnValueChanged);
        QuestManager.instance.onQuestCompleted.AddListener(OnQuestCompleted);

        questContent = qc;
        slider.minValue = 0;
        slider.maxValue = questContent.quest.amount;
        for (int i = 0; i < qc.item.Length; i++)
        {
            Image image = Instantiate(reward, rewardHolder).GetComponent<Image>();
            image.sprite = Resources.Load<Sprite>(qc.item[i].inventoryImage);
            TextMeshProUGUI textAmount = image.GetComponentInChildren<TextMeshProUGUI>();
            textAmount.text = 1.ToString();
        }
        completeButton.GetComponent<Button>().onClick.AddListener(OnCompleteButtonClick);
        OnValueChanged();
    }

    public void OnValueChanged()
    {
        if (questContent.quest.state != QuestState.Progress) return;

        slider.value = questContent.currentAmount;
        sliderText.text = $"{questContent.currentAmount} / {questContent.quest.amount}";
    }

    public void OnQuestCompleted()
    {
        if (questContent.quest.state != QuestState.Complete) return;

        background.sprite = completeBackground;
        completeButton.SetActive(true);
        slider.gameObject.SetActive(false);
        sliderText.gameObject.SetActive(false);
        rewardHolder.anchoredPosition = new Vector2(-270, 0);
    }

    public void OnCompleteButtonClick()
    {
        if (questContent.quest.state != QuestState.Complete) return;

        QuestManager.instance.RecieveRewards(questContent);
        completeButton.SetActive(false);
        rewardHolder.anchoredPosition = new Vector2(0, 0);

        transform.SetSiblingIndex(QuestManager.instance.playerQuestList.Count);
    }
}
