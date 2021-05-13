using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTester : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(MotionChanger());
    }

    IEnumerator MotionChanger()
    {
        while (true)
        {
            animator.SetBool("Front", false);
            yield return new WaitForSeconds(2);
            animator.SetBool("Move", true);
            yield return new WaitForSeconds(2);
            animator.SetBool("Move", false);
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(2);
            animator.SetBool("Front", true);
            yield return new WaitForSeconds(2);
            animator.SetBool("Move", true);
            yield return new WaitForSeconds(2);
            animator.SetBool("Move", false);
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(2);
            animator.SetBool("Move", false);
            yield return new WaitForSeconds(2);

        }
    }
}
