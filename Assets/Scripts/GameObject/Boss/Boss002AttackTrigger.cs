using System.Collections;
using UnityEngine;

public class Boss002AttackTrigger : MonoBehaviour
{
    [SerializeField]
    private Boss002 boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LazyCamera.instance.SetCameraSize(10f);
            StartCoroutine("WaitTime", 1f);
        }
    }

    private IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);

        boss.CanAttack = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LazyCamera.instance.ResetCameraSize();
            boss.CanAttack = false;
        }
    }
}
