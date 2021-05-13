using UnityEngine;

public class OvalPattern : MonoBehaviour
{
    [SerializeField]
    private GameObject[] skillObjects = new GameObject[0];
    [SerializeField]
    [Range(0f, 1f)]
    private float sizeX = 1, sizeY = 1;

    private void Start()
    {
        Execute();
    }

    public void Execute()
    {
        for (int i = 0; i < skillObjects.Length; i++)
        {
            float angle = 360f / skillObjects.Length * i * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(angle) * sizeX, Mathf.Sin(angle) * sizeY, 0);
            Vector3 pos = transform.position + dir * 1;
            ProjectileMove projectileMove;
            if (skillObjects[i].TryGetComponent(out projectileMove) == false)
            {
                projectileMove = skillObjects[i].AddComponent<ProjectileMove>();
            }
            projectileMove.SetDir(dir);
            skillObjects[i].transform.position = pos;
        }
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < skillObjects.Length; i++)
        {
            float angle = 360f / skillObjects.Length * i * Mathf.Deg2Rad;
            Vector3 pos = transform.position + new Vector3(Mathf.Cos(angle) * sizeX, Mathf.Sin(angle) * sizeY, 0);
            Gizmos.DrawWireSphere(pos, 0.1f);
        }
    }
}
