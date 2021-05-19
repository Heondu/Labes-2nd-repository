using System.Collections;
using UnityEngine;

public class LimbSeparator : MonoBehaviour
{
    private Enemy enemy;

    [SerializeField]
    private float diffusivity = 10;
    [SerializeField]
    private float minForce = 10;
    [SerializeField]
    private float maxForce = 15;
    [SerializeField]
    private float floatTime = 2;
    [SerializeField]
    private float waitTime = 5;
    [SerializeField]
    private GameObject minimapIcon;
    [SerializeField]
    private GameObject Shadow;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        enemy.onDeath.AddListener(OnDeath);
    }

    private void OnDeath()
    {
        StartCoroutine("OnDeathCo", enemy.hitDir);
    }

    private IEnumerator OnDeathCo(Vector3 hitDir)
    {

        minimapIcon.SetActive(false);
        Shadow.SetActive(false);

        Rigidbody2D[] rigidbody2Ds = GetComponentsInChildren<Rigidbody2D>();
        Vector3[] originPos = new Vector3[rigidbody2Ds.Length - 1];
        hitDir.y = Mathf.Min(1, hitDir.y + 1);

        for (int i = 1; i < rigidbody2Ds.Length; i++)
        {
            originPos[i - 1] = rigidbody2Ds[i].transform.localPosition;

            float force = Random.Range(minForce, maxForce);
            float angle = Mathf.Atan2(hitDir.y, hitDir.x) * Mathf.Rad2Deg;
            float newAngle = Random.Range(angle - diffusivity, angle + diffusivity) * Mathf.Deg2Rad;
            Vector2 newDir = new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));

            rigidbody2Ds[i].AddForce(newDir * force, ForceMode2D.Impulse);
            rigidbody2Ds[i].gravityScale = 1;

            StartCoroutine(StopMove(rigidbody2Ds[i], force / maxForce * floatTime));
            //StartCoroutine(Rotate(rigidbody2Ds[i].transform, force / maxForce * floatTime));
        }

        yield return new WaitForSeconds(waitTime);

        gameObject.SetActive(false);
        minimapIcon.SetActive(true);
        Shadow.SetActive(true);

        for (int i = 1; i < rigidbody2Ds.Length; i++)
        {
            rigidbody2Ds[i].transform.localPosition = originPos[i - 1];
        }
    }

    private IEnumerator StopMove(Rigidbody2D target, float time)
    {
        yield return new WaitForSeconds(time);

        target.gravityScale = 0;
        target.velocity = Vector3.zero;
    }

    private IEnumerator Rotate(Transform target, float time)
    {
        float angle = Mathf.Sign(Random.Range(-1, 1));

        float current = 0;
        while (current < time)
        {
            current += Time.deltaTime;

            target.Rotate(new Vector3(0, 0, angle));

            yield return null;
        }
    }
}
