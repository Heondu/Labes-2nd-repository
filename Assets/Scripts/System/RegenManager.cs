using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RegenManager : MonoBehaviour
{
    [SerializeField]
    private float regenTime = 10;
    private float currentTime = 0;

    public RegenArea[] regens = new RegenArea[1];

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
        currentTime = 0;

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
                        yield return WaitForSeconds(regenTime);

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
                    yield return WaitForSeconds(regenTime);

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

    private IEnumerator WaitForSeconds(float seconds)
    {
        while (currentTime < seconds)
        {
            currentTime += Time.deltaTime;

            yield return null;
        }
    }

    public void SetActiveRegenArea(RegenArea regen)
    {
        if (regens == null && regens.Length < 1)
        {
            regens = new RegenArea[1];
        }
        
        regens[0] = regen;
        
        StopCoroutine("Regen");
        StartCoroutine("Regen");
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
