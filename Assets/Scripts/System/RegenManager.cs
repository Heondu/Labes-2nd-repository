using System.Collections;
using UnityEngine;

public class RegenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bossHPBar;
    [SerializeField]
    private GameObject monsterHPBar;
    [SerializeField]
    private Transform monsterHPCanvas;

    [SerializeField]
    private float regenTime = 10;

    private RegenArea[] regens;

    private void Awake()
    {
        regens = FindObjectsOfType<RegenArea>();

        foreach (RegenArea regen in regens)
        {
            Spawn(regen);
        }

    }

    private void Start()
    {
        StartCoroutine("Regen");
    }

    private void Spawn(RegenArea regenArea)
    {
        regenArea.GetComponent<RegenMonster>().SpawnMonster(regenArea, monsterHPBar, monsterHPCanvas, bossHPBar);
    }

    private IEnumerator Regen()
    {
        while (true)
        {
            foreach (RegenArea regen in regens)
            {
                if (regen.GetComponentsInChildren<Enemy>(false).Length == 0)
                {
                    yield return new WaitForSeconds(regenTime);

                    Spawn(regen);
                }
            }

            yield return null;
        }
    }
}
