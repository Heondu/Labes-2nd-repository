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
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI content;
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
        title.text = qc.quest.title;
        string newContent = qc.quest.content.Replace("type", DataManager.Localization(qc.quest.type));
        newContent = newContent.Replace("amount", qc.quest.amount.ToString());
        content.text = newContent;

        for (int i = 0; i < qc.item.Length; i++)
        {
            Image image = Instantiate(reward, rewardHolder).GetComponent<Image>();
            image.sprite = Resources.Load<Sprite>(qc.item[i].inventoryImage);
            TextMeshProUGUI textAmount = image.GetComponentInChildren<TextMeshProUGUI>();
            textAmount.text = 1.ToString();
        }

        if (qc.quest.state == QuestState.Progress)
        {
            QuestManager.instance.onValueChanged.AddListener(OnValueChanged);

            questContent = qc;
            slider.minValue = 0;
            slider.maxValue = questContent.quest.amount;

            completeButton.GetComponent<Button>().onClick.AddListener(OnCompleteButtonClick);
            OnValueChanged();
        }
        else if (qc.quest.state == QuestState.Complete)
        {
            background.sprite = completeBackground;
            slider.gameObject.SetActive(false);
            sliderText.gameObject.SetActive(false);
            transform.SetSiblingIndex(QuestManager.instance.playerQuestList.Count);
        }
    }

    public void OnValueChanged()
    {
        slider.value = questContent.currentAmount;
        sliderText.text = $"{questContent.currentAmount} / {questContent.quest.amount}";

        if (questContent.quest.amount <= questContent.currentAmount)
        {
            Debug.Log($"{questContent.quest.name} -COMPLETE-");
            OnQuestCompleted();
        }
    }

    public void OnQuestCompleted()
    {
        QuestManager.instance.onValueChanged.RemoveListener(OnValueChanged);

        background.sprite = completeBackground;
        completeButton.SetActive(true);
        slider.gameObject.SetActive(false);
        sliderText.gameObject.SetActive(false);
        rewardHolder.anchoredPosition = new Vector2(-270, 0);

    }

    public void OnCompleteButtonClick()
    {
        QuestManager.instance.RecieveRewards(questContent);
        completeButton.SetActive(false);
        rewardHolder.anchoredPosition = new Vector2(0, 0);

        transform.SetSiblingIndex(QuestManager.instance.playerQuestList.Count);
    }
}
