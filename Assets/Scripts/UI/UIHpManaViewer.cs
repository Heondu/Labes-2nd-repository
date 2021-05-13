using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHpManaViewer : MonoBehaviour
{
    [SerializeField]
    private Image imageHP;
    [SerializeField]
    private Image imageMana;
    [SerializeField]
    private TextMeshProUGUI textHP;
    [SerializeField]
    private TextMeshProUGUI textMana;
    private PlayerStatus playerStatus;

    private void Awake()
    {
        playerStatus = FindObjectOfType<Player>().status;
    }

    private void Update()
    {
        imageHP.fillAmount = (float)playerStatus.HP / playerStatus.maxHP;
        if (textHP != null)
            textHP.text = $"{playerStatus.HP}/{playerStatus.maxHP}";
        imageMana.fillAmount = (float)playerStatus.mana / playerStatus.maxMana;
        if (textMana != null)
            textMana.text = $"{playerStatus.mana}/{playerStatus.maxMana}";
    }
}
