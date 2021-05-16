using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemySwarmController : MonoBehaviour
{
    public UnityEvent onSwarmAttackActive = new UnityEvent();
    public UnityEvent onSwarmAttackInactive = new UnityEvent();

    private EnemyController[] enemys;
    private bool isKeepSwarmAttack = false;

    private void Awake()
    {
        onSwarmAttackActive.AddListener(OnSwarmAttack);
    }

    public void Init(Transform target, bool isSwarmAttack)
    {
        enemys = GetComponentsInChildren<EnemyController>();
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetTarget(target);
        }

        isKeepSwarmAttack = isSwarmAttack;
        if (isSwarmAttack == true)
            onSwarmAttackActive.Invoke();
    }

    private void OnSwarmAttack()
    {
        if (isKeepSwarmAttack == false)
        {
            StopCoroutine("SetSwarmAttack");
            StartCoroutine("SetSwarmAttack");
        }
    }

    private IEnumerator SetSwarmAttack()
    {
        yield return new WaitForSeconds(5f);

        onSwarmAttackInactive.Invoke();
    }
}
