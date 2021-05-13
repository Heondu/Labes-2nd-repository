using System.Collections;
using UnityEngine;

public class EnemySwarmController : MonoBehaviour
{
    private EnemyController[] enemys;
    private bool isSwarmAttack = false;

    public void Init()
    {
        enemys = GetComponentsInChildren<EnemyController>();
    }

    private void Update()
    {
        if (isSwarmAttack) return;
        if (enemys == null) return;
        for (int i = 0; i < enemys.Length; i++)
        {
            if (enemys[i].isSwarmAttack)
            {
                isSwarmAttack = true;
                StopCoroutine("SetSwarmAttack");
                StartCoroutine("SetSwarmAttack");
            }
        }
    }

    private IEnumerator SetSwarmAttack()
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].isSwarmAttack = true;
        }

        yield return new WaitForSeconds(5f);

        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].isSwarmAttack = false;
        }

        isSwarmAttack = false;
    }
}
