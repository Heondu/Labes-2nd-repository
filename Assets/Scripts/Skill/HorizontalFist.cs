using System.Collections;
using UnityEngine;

public class HorizontalFist : MonoBehaviour
{
    private SkillData skillData;
    private Transform player;
    private Animator animator;
    private new CapsuleCollider2D collider2D;

    [SerializeField]
    private GameObject fistArea;
    private GameObject fistAreaClone;
    [SerializeField]
    private float alphaTime = 0.5f;

    [SerializeField]
    private float startX = 0f;
    [SerializeField]
    private float limitX = 0f;
    [SerializeField]
    private float colliderX = 0f;

    private void Start()
    {
        skillData = GetComponent<SkillData>();
        player = FindObjectOfType<Player>().transform;
        animator = GetComponent<Animator>();
        collider2D = GetComponent<CapsuleCollider2D>();

        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.position = new Vector3(LazyCamera.instance.transform.position.x + LazyCamera.instance.GetWidth() / 2 * -1, player.position.y, transform.position.z);
        startX = LazyCamera.instance.transform.position.x + LazyCamera.instance.GetWidth() / 2 * -1;
        limitX = LazyCamera.instance.transform.position.x + LazyCamera.instance.GetWidth() / 2;
        StartCoroutine("FistArea");

        StartCoroutine("MoveToX");
    }

    private IEnumerator FistArea()
    {
        Vector3 pos = new Vector3(LazyCamera.instance.transform.position.x - collider2D.offset.x, player.position.y - collider2D.offset.y, transform.position.z);
        fistAreaClone = Instantiate(fistArea, pos, Quaternion.identity);
        fistAreaClone.transform.localScale = new Vector3(limitX - startX, fistAreaClone.transform.localScale.y, fistAreaClone.transform.localScale.z);
        SpriteRenderer sr = fistAreaClone.GetComponent<SpriteRenderer>();
        Color color = sr.color;

        float percent = 0;
        float current = 0;
        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / alphaTime;
            float alpha = Mathf.Lerp(0, 150, percent);
            color.a = alpha / 255f;
            if (sr != null) sr.color = color;
            yield return null;
        }
    }

    private IEnumerator MoveToX()
    {
        yield return new WaitForSeconds(1f);

        Destroy(fistAreaClone);

        float percent = 0;
        float current = 0;
        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current * skillData.speed;
            float newX = Mathf.Lerp(startX, limitX, percent);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);

            yield return null;
        }

        animator.SetTrigger("Attack");
        skillData.executor.GetComponent<Boss002>().animator.Attack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < skillData.nextSkills.Length; i++)
            {
                SkillLoader.instance.LoadSkill(skillData, skillData.nextSkills[i], transform.position, transform.up);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(new Vector3(colliderX, transform.position.y, 0), 0.1f);
    }
}
