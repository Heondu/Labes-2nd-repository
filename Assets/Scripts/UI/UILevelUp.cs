using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILevelUp : MonoBehaviour
{
    private Player player;
    [SerializeField]
    private GameObject levelUpObject;
    [SerializeField]
    private TextMeshProUGUI textLevelUp;
    [SerializeField]
    private float floatingTime = 2f;

    private void Awake()
    {
        player = FindObjectOfType<Player>();

        player.onLevelUp.AddListener(PopupLevelUp);
    }

    private void PopupLevelUp()
    {
        textLevelUp.text = $"{player.GetValue("level")}레벨 달성!";

        levelUpObject.SetActive(true);

        StartCoroutine("DisablePopup");
    }

    private IEnumerator DisablePopup()
    {
        yield return new WaitForSeconds(floatingTime);

        levelUpObject.SetActive(false);
    }
}
