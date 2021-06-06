using UnityEngine;

public class Boss002Animator : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private float lookRange = 4;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CheckForXPos(float xPos)
    {
        if (xPos < transform.position.x - lookRange)
        {
            animator.SetInteger("Dir", -1);
        }
        else if (xPos > transform.position.x + lookRange)
        {
            animator.SetInteger("Dir", 1);
        }
        else
        {
            animator.SetInteger("Dir", 0);
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
