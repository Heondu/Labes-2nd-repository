using System.Collections;
using UnityEngine;

public class HorizontalFist : MonoBehaviour
{
    private SkillData skillData;
    private Transform player;
    private Animator animator;

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

        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.position = new Vector3(LazyCamera.instance.GetWidth() / 2 * -1, player.position.y, transform.position.z);

        animator.SetInteger("Dir", (int)Mathf.Sign(player.position.x));

        StartCoroutine("MoveToX");
    }

    private IEnumerator MoveToX()
    {
        startX = LazyCamera.instance.GetWidth() / 2 * -1;
        limitX = LazyCamera.instance.GetWidth() / 2;

        yield return new WaitForSeconds(1f);

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
