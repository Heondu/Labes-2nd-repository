using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTester1 : MonoBehaviour
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
            animator.SetTrigger("Special");
            yield return new WaitForSeconds(5);

        }
    }
}
