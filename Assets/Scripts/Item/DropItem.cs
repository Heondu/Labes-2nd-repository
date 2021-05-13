using System.Collections;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    private bool isMove = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Diffusion(int weight)
    {
        Vector2 dir = Random.insideUnitCircle;

        rigidbody.mass = weight;

        rigidbody.AddForce(dir * 10, ForceMode2D.Impulse);
    }

    public void MoveToPlayer(Transform target)
    {
        if (isMove == false)
        {
            isMove = true;

            rigidbody.velocity = Vector3.zero;

            StartCoroutine(Move(target));
        }
    }

    private IEnumerator Move(Transform target)
    {
        while (true)
        {
            transform.Translate((target.position - transform.position).normalized * 10 * Time.deltaTime);

            yield return null;
        }
    }
}
