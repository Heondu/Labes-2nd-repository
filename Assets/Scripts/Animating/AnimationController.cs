using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Vector3 flipRight;
    private Vector3 flipLeft;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        flipRight = new Vector3(-Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
        flipLeft = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
    }

    public void Movement(Vector3 axis)
    {
        if (axis != Vector3.zero && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_rear"))
        {
            animator.SetBool("Move", true);
            if (axis.x > 0) transform.localScale = flipRight;
            else if (axis.x < 0) transform.localScale = flipLeft;
            if (axis.y > 0) animator.SetBool("Front", false);
            else if (axis.y < 0) animator.SetBool("Front", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }

    public void Attack(Vector2 dir)
    {
        if (dir.x > 0) transform.localScale = flipRight;
        else if (dir.x < 0) transform.localScale = flipLeft;
        if (dir.y > 0) animator.SetBool("Front", false);
        else if (dir.y < 0) animator.SetBool("Front", true);
        animator.SetTrigger("Attack");
    }

    public void Enable(bool value)
    {
        if (animator != null)
            animator.enabled = value;
    }
}
