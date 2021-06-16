using System.Collections;
using UnityEngine;

public class VerticalFist : MonoBehaviour
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
    private float startY = 44f;
    [SerializeField]
    private float limitY = 28f;
    [SerializeField]
    private float colliderY = 0f;

    private void Start()
    {
        skillData = GetComponent<SkillData>();
        player = FindObjectOfType<Player>().transform;
        animator = GetComponent<Animator>();
        collider2D = GetComponent<CapsuleCollider2D>();

        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.position = new Vector3(player.position.x, startY, transform.position.z);
        StartCoroutine("FistArea");

        animator.SetInteger("Dir", (int)Mathf.Sign(player.position.x));

        StartCoroutine("MoveToY");
    }

    private IEnumerator FistArea()
    {
        Vector3 pos = new Vector3(player.position.x - collider2D.offset.x, player.position.y - colliderY - collider2D.offset.y, transform.position.z);
        fistAreaClone = Instantiate(fistArea, pos, Quaternion.identity);
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

    private IEnumerator MoveToY()
    {
        float playerY = player.position.y - colliderY;
        float transformY = transform.position.y;

        yield return new WaitForSeconds(1f);

        Destroy(fistAreaClone);

        float percent = 0;
        float current = 0;
        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current * skillData.speed;
            float newY = Mathf.Lerp(transformY, playerY, percent);
            newY = Mathf.Max(limitY, newY);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

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
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, colliderY, 0), 0.1f);
    }
}
