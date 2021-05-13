using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIExpViewer : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI textLevel;
    [SerializeField]
    private TextMeshProUGUI textExp;
    private PlayerStatus playerStatus;

    private void Awake()
    {
        playerStatus = FindObjectOfType<Player>().status;
    }

    private void Update()
    {
        image.fillAmount = playerStatus.exp / (int)DataManager.experience[playerStatus.level]["exp"];
        textLevel.text = playerStatus.level.ToString();
        textExp.text = (playerStatus.exp / (int)DataManager.experience[playerStatus.level]["exp"] * 100).ToString("0.##") + "%";
    }
}
