using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(ie());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator ie()
    {
        anim.SetTrigger("Toggle");
        yield return new WaitForSeconds(5);
        anim.SetTrigger("Toggle");
        yield return new WaitForSeconds(5);
        anim.SetTrigger("Toggle");
        yield return new WaitForSeconds(5);
        anim.SetTrigger("Toggle");
    }
}
