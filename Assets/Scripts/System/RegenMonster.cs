using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RegenMonster : MonoBehaviour
{
    private GameObject[] spawnObjs = null;

    public void SpawnMonster(RegenArea regenArea, Transform target, bool isSwarmAttack, UnityAction actionOnDeath = null)
    {
        if (spawnObjs == null)
            spawnObjs = new GameObject[regenArea.maxRegenNum];
        List<GameObject> objs = RandomSelect(regenArea.monsters, regenArea.prob, regenArea.maxRegenNum);
        List<GameObject> enemyObjs = new List<GameObject>();
        List<int> enemyProbs = new List<int>();

        for (int i = 0; i < regenArea.monsters.Length; i++)
        {
            Enemy enemy = regenArea.monsters[i].GetComponent<Enemy>();
            if (enemy != null)
            {
                enemyObjs.Add(regenArea.monsters[i]);
                enemyProbs.Add(regenArea.prob[i]);
            }
        }

        for (int i = 0; i < regenArea.maxRegenNum; i++)
        {
            if (IsExist(i)) continue;

            Enemy enemy = objs[i].GetComponent<Enemy>();
            if (enemy == null)
            {
                GameObject clone = ObjectPooler.instance.ObjectPool(regenArea.transform, objs[i]);
                SetPos(regenArea, clone, null, i, regenArea.maxRegenNum, false);
                spawnObjs[i] = clone;
            }
            else
            {
                List<GameObject> enemys = RandomSelect(enemyObjs.ToArray(), enemyProbs.ToArray(), regenArea.maxEnemySwarmNum);
                EnemySwarmController enemyHolder = new GameObject("EnemyHolder").AddComponent<EnemySwarmController>();
                enemyHolder.transform.SetParent(regenArea.transform);
                SetPos(regenArea, enemyHolder.gameObject, null, i, regenArea.maxRegenNum, false);
                spawnObjs[i] = enemyHolder.gameObject;

                for (int j = 0; j < regenArea.maxEnemySwarmNum; j++)
                {
                    Enemy clone = ObjectPooler.instance.ObjectPool(enemyHolder.transform, enemys[j]).GetComponent<Enemy>();
                    SetPos(regenArea, clone.gameObject, enemyHolder.transform, j, regenArea.maxEnemySwarmNum, true);
                    SetupMonster(regenArea, clone, actionOnDeath);
                }
                enemyHolder.Init(target, isSwarmAttack);
            }
        }
    }

    private List<GameObject> RandomSelect(GameObject[] objs, int[] probs, int maxRegenNum)
    {
        List<GameObject> objList = new List<GameObject>();
        int sumOfProb = 0;
        for (int i = 0; i < probs.Length; i++)
        {
            sumOfProb += probs[i];
        }

        for (int i = 0; i < maxRegenNum; i++)
        {
            int rand = Random.Range(0, sumOfProb);
            int sum = 0;
            int index = 0;
            for (int j = 0; j < probs.Length; j++)
            {
                sum += probs[j];
                if (rand < sum)
                {
                    index = j;
                    break;
                }
            }
            objList.Add(objs[index]);
        }

        return objList;
    }

    private void SetPos(RegenArea regenArea, GameObject obj, Transform holder, int index, int maxNum, bool isEnemySpawn)
    {
        int column = (int)Mathf.Sqrt(maxNum);
        int row = Mathf.CeilToInt(Mathf.Sqrt(maxNum));
        float areaXDistance = regenArea.area.x / row;
        float areaYDistance = regenArea.area.y / column;
        int x = index / row;
        int y = index % row;

        Vector3 newPos;
        float offsetX;
        float offsetY;
        if (!isEnemySpawn)
        {
            offsetX = (-(float)row / 2 + 0.5f + x) * areaXDistance;
            offsetY = (-(float)column / 2 + 0.5f + y) * areaYDistance;
            newPos = regenArea.transform.position + new Vector3(offsetX, offsetY, 0);
        }
        else
        {
            Bounds enemyBounds = obj.GetComponent<Collider2D>().bounds;
            offsetX = (-(float)row / 2 + 0.5f + x) * enemyBounds.size.x;
            offsetY = (-(float)column / 2 + 0.5f + y) * enemyBounds.size.y;
            if (holder != null)
                newPos = holder.position + new Vector3(offsetX, offsetY, 0);
            else
                newPos = regenArea.transform.position + new Vector3(offsetX, offsetY, 0);
        }
        obj.transform.position = newPos;
    }

    private void SetupMonster(RegenArea regenArea, Enemy enemy, UnityAction actionOnDeath)
    {
        int eliteRand = Random.Range(0, 100);
        if (eliteRand < regenArea.eliteProb)
            enemy.Init(actionOnDeath, true);
        else
            enemy.Init(actionOnDeath, false);
        enemy.GetComponent<EnemyController>().SetPos();

        if (enemy.monster["class"].ToString() == "pawn" && eliteRand >= regenArea.eliteProb)
            UIMonsterHP.instance.InitMonsterHPBar(enemy);
        else
            UIMonsterHP.instance.InitBossHPBar(enemy);
    }

    private bool IsExist(int index)
    {
        if (spawnObjs[index] == null) return false;
        if (spawnObjs[index].activeSelf == false) return false;
        if (spawnObjs[index].name.Equals("EnemyHolder") == true) return false;

        return true;
    }
}