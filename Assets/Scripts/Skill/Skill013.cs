using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill013 : MonoBehaviour
{
    private SkillData skillData;
    private Transform player;
    private Animator animator;

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

        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.position = new Vector3(player.position.x, startY, transform.position.z);

        animator.SetInteger("Dir", (int)Mathf.Sign(player.position.x));

        StartCoroutine("MoveToY");
    }

    private IEnumerator MoveToY()
    {
        float playerY = player.position.y - colliderY;
        float transformY = transform.position.y;

        yield return new WaitForSeconds(1f);

        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime;
            float newY = Mathf.Lerp(transformY, playerY, percent);
            newY = Mathf.Max(limitY, newY);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
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
