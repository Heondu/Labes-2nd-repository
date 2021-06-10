using System.Collections;
using UnityEngine;
using TMPro;

public class FloatingDamage : MonoBehaviour
{
    private float moveSpeed = 1f;
    private float fadeOutTime = 1f;
    private float destroyTime = 2f;
    private float distance = 50f;
    private TextMeshProUGUI damageText;
    private Color alpha = Color.white;
    private Vector3 originPos;
    private Vector3 offset = Vector3.up;
    private GameObject executor;

    private void Awake()
    {
        damageText = GetComponent<TextMeshProUGUI>();
        Destroy(gameObject, destroyTime);
    }

    public void Init(GameObject executor, string damage, Vector3 originPos)
    {
        this.executor = executor;
        this.originPos = originPos;
        damageText.text = damage;
        offset.x += Random.Range(-0.5f, 0.5f);
        StartCoroutine("FadeOut");
    }

    private void Update()
    {
        offset += Vector3.up * moveSpeed * Time.deltaTime;
        Vector3 newPos = originPos + offset;
        newPos.z = 0;
        transform.position = newPos;
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(destroyTime - fadeOutTime);

        float percent = 0;
        while (percent < 1)
        {
            alpha.a = Mathf.Lerp(alpha.a, 0, percent);
            damageText.color = alpha;

            percent += Time.deltaTime / fadeOutTime;
            yield return null;
        }
    }

    public void SetPos(float moveValue)
    {
        offset += Vector3.up * (moveValue / distance);
    }

    private void OnDestroy()
    {
        FloatingDamageManager.instance.RemoveDamage(executor, this);
    }

    private void OnLevelWasLoaded(int level)
    {
        Destroy(gameObject);
    }
}
