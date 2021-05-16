using UnityEngine;

public class UIMonsterHP : MonoBehaviour
{
    public static UIMonsterHP instance;

    [SerializeField]
    private GameObject monsterHPBar;
    [SerializeField]
    private UIBossHPViewer bossHPBar;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void InitMonsterHPBar(Enemy enemy)
    {
        GameObject clone = ObjectPooler.instance.ObjectPool(transform, monsterHPBar);
        clone.GetComponent<UIMonsterHPViewer>().Init(enemy.transform, enemy.status);
    }

    public void InitBossHPBar(Enemy enemy)
    {
        bossHPBar.Init(enemy.transform, enemy.status, DataManager.Localization(enemy.GetID()));
    }
}
