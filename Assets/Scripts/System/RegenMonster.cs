using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RegenMonster : MonoBehaviour
{
    public void SpawnMonster(RegenArea regenArea, Transform target, bool isSwarmAttack, UnityAction actionOnDeath = null)
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
            enemys.Add(regenArea.monsters[index]);
        }

        int enemyColumn = (int)Mathf.Sqrt(regenArea.maxRegenNum);
        int enemyRow = Mathf.CeilToInt(Mathf.Sqrt(regenArea.maxRegenNum));
        float areaXDistance = regenArea.area.x / enemyRow;
        float areaYDistance = regenArea.area.y / enemyColumn;

        for (int x = 0; x < enemyRow; x++)
        {
            for (int y = 0; y < enemyColumn; y++)
            {
                int index = x * enemyColumn + y;
                if (index >= regenArea.maxRegenNum) continue;

                GameObject clone = ObjectPooler.instance.ObjectPool(regenArea.transform, enemys[index]);

                Vector3 newPos;
                float offsetX;
                float offsetY;
                if (regenArea.autoArea == false)
                {
                    offsetX = (-(float)enemyRow / 2 + 0.5f + x) * areaXDistance;
                    offsetY = (-(float)enemyColumn / 2 + 0.5f + y) * areaYDistance;
                    newPos = regenArea.transform.position + new Vector3(offsetX, offsetY, 0);
                }
                else
                {
                    Bounds enemyBounds = clone.GetComponent<Collider2D>().bounds;
                    offsetX = (-(float)enemyRow / 2 + 0.5f + x) * enemyBounds.size.x;
                    offsetY = (-(float)enemyColumn / 2 + 0.5f + y) * enemyBounds.size.y;
                    newPos = regenArea.transform.position + new Vector3(offsetX, offsetY, 0);
                }
                clone.transform.position = newPos;

                Enemy enemy = clone.GetComponent<Enemy>();
                if (enemy != null)
                {
                    int eliteRand = Random.Range(0, 100);
                    if (eliteRand < regenArea.eliteProb)
                        enemy.Init(actionOnDeath, true);
                    else
                        enemy.Init(actionOnDeath, false);
                    clone.GetComponent<EnemyController>().SetPos();

                    if (enemy.monster["class"].ToString() == "pawn" && eliteRand >= regenArea.eliteProb)
                        UIMonsterHP.instance.InitMonsterHPBar(enemy);
                    else
                        UIMonsterHP.instance.InitBossHPBar(enemy);
                }
            }
        }
        GetComponent<EnemySwarmController>().Init(target, isSwarmAttack);
    }
}