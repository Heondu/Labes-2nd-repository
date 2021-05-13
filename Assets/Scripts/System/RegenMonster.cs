using System.Collections.Generic;
using UnityEngine;

public class RegenMonster : MonoBehaviour
{
    public void SpawnMonster(RegenArea regenArea)
    {
        List<GameObject> enemys = new List<GameObject>();

        int sumOfProb = 0;
        for (int i = 0; i < regenArea.prob.Length; i++)
        {
            sumOfProb += regenArea.prob[i];
        }

        for (int i = 0; i < regenArea.maxRegenNum; i++)
        {
            int rand = Random.Range(0, sumOfProb);
            int sum = 0;
            int index = 0;
            for (int j = 0; j < regenArea.prob.Length; j++)
            {
                sum += regenArea.prob[j];
                if (rand < sum)
                {
                    index = j;
                    break;
                }
            }

            GameObject clone = Instantiate(regenArea.monsters[index], transform);
            clone.GetComponent<Enemy>().Init();
            enemys.Add(clone);
        }

        int enemyColumn = (int)Mathf.Sqrt(regenArea.maxRegenNum);
        int enemyRow = Mathf.CeilToInt(Mathf.Sqrt(regenArea.maxRegenNum));
        for (int x = 0; x < enemyRow; x++)
        {
            for (int y = 0; y < enemyColumn; y++)
            {
                int index = x * enemyColumn + y;
                if (index >= regenArea.maxRegenNum) continue;

                Bounds enemyBounds = enemys[index].GetComponent<CapsuleCollider2D>().bounds;
                Vector3 newPos = regenArea.transform.position + new Vector3(enemyBounds.size.x * x, enemyBounds.size.y * y, 0);
                enemys[index].GetComponent<EnemyController>().SetPos(newPos);
            }
        }
        GetComponent<EnemySwarmController>().Init();
    }
}
