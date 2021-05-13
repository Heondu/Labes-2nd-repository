using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMonsterHPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lvText;
    [SerializeField]
    private Image hpImage;
    private Transform target;
    private EnemyStatus enemyStatus;
    private float yOffset = -0.2f;

    public void Init(Transform target, EnemyStatus enemyStatus)
    {
        this.target = target;
        this.enemyStatus = enemyStatus;

        lvText.text = enemyStatus.level.ToString();

        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (target != null)
        {
            hpImage.fillAmount = (float)enemyStatus.HP / enemyStatus.maxHP;

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

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y + yOffset, 0);
        }
    }
}
