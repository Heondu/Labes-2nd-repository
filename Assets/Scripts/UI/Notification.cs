using UnityEngine;
using TMPro;

public class Notification : MonoBehaviour
{
    [SerializeField]
    private GameObject notification;
    [SerializeField]
    private TextMeshProUGUI textCount;
    private int count = 0;

    public void Notify(bool isNotified)
    {
        notification.SetActive(isNotified);
    }

    public void ClearText()
    {
        if (textCount == null) return;

        textCount.text = "";
        count = 0;
    }

    public void IncreaseNum()
    {
        if (textCount == null) return;

        count++;
        textCount.text = count.ToString();
    }
}
