using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBossHPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    private Slider hpSlider;
    private Transform target;
    private EnemyStatus enemyStatus;
    public void Init(Transform target, EnemyStatus enemyStatus, string name)
    {
        this.target = target;
        this.enemyStatus = enemyStatus;

        nameText.text = name;
        hpSlider = GetComponent<Slider>();

        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (target != null)
        {
            hpSlider.value = (float)enemyStatus.HP / enemyStatus.maxHP;

            if (enemyStatus.HP == 0)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
