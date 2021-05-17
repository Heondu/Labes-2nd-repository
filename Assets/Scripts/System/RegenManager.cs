using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RegenManager : MonoBehaviour
{
    [SerializeField]
    private float regenTime = 10;

    private RegenArea[] regens;

    [SerializeField]
    private Transform target;
    [SerializeField]
    private bool swarmAttackAtStart = false;
    [SerializeField]
    private bool checkAllRegenAreaWhenRegen = false;

    public UnityEvent onRegen = new UnityEvent();
    public UnityEvent onEnemyDeath = new UnityEvent();

    private void Start()
    {
        regens = FindObjectsOfType<RegenArea>();

        StartCoroutine("Regen");
    }

    private void Update()
    {
        FindTarget();
    }

    private void FindTarget()
    {
        if (target == null)
            target = FindObjectOfType<Player>().transform;
    }

    private void Spawn(RegenArea regenArea)
    {
        if (regenArea == null) return;

        FindTarget();
        regenArea.GetComponent<RegenMonster>().SpawnMonster(regenArea, target, swarmAttackAtStart, OnEnemyDeath);
    }

    private IEnumerator Regen()
    {
        yield return new WaitForSeconds(1f);

        foreach (RegenArea regen in regens)
        {
            Spawn(regen);
        }

        while (true)
        {
            if (checkAllRegenAreaWhenRegen == false)
            {
                foreach (RegenArea regen in regens)
                {
                    if (regen.GetComponentsInChildren<Enemy>(false).Length == 0)
                    {
                        yield return new WaitForSeconds(regenTime);

                        Spawn(regen);

                        onRegen.Invoke();
                    }
                }
            }
            else
            {
                bool flag = true;
                foreach (RegenArea regen in regens)
                {
                    if (regen.GetComponentsInChildren<Enemy>(false).Length != 0)
                    {
                        flag = false;
                    }
                }

                if (flag == true)
                {
                    yield return new WaitForSeconds(regenTime);

                    foreach (RegenArea regen in regens)
                    {
                        Spawn(regen);
                    }

                    onRegen.Invoke();
                }
            }

            yield return null;
        }
    }

    public void OnEnemyDeath()
    {
        onEnemyDeath.Invoke();
    }

    public Transform GetTarget()
    {
        return target;
    }
}
