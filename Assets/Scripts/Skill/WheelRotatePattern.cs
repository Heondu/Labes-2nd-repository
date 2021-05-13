using UnityEngine;

[RequireComponent(typeof(ProjectileMove))]
[RequireComponent(typeof(ProjectileRotate))]
public class WheelRotatePattern : MonoBehaviour
{
    [SerializeField]
    private GameObject[] skillObjects = new GameObject[0];
    [SerializeField]
    private float angle = 60;
    [SerializeField]
    private float distance = 1;
    private SkillData skillData;
    private ProjectileMove projectileMove;
    private ProjectileRotate projectileRotate;

    private void Start()
    {
        Execute();
    }

    public void Execute()
    {
        skillData = GetComponent<SkillData>();
        projectileMove = GetComponent<ProjectileMove>();
        projectileRotate = GetComponent<ProjectileRotate>();

        projectileMove.SetDir(transform.up);
        projectileMove.SetSpeed(skillData.speed);
        projectileRotate.repeat = true;

        for (int i = 0; i < skillObjects.Length; i++)
        {
            float angle = 360f / skillObjects.Length * i * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            Vector3 pos = transform.position + dir * distance;
            skillObjects[i].transform.position = pos;

            ProjectileRotate projectileRotate;
            if (skillObjects[i].TryGetComponent(out projectileRotate) == false)
            {
                projectileRotate = skillObjects[i].AddComponent<ProjectileRotate>();
            }
            projectileRotate.Rotate(Rotation.GetAngle(dir) + this.projectileRotate.GetSign() * this.angle);
        }
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < skillObjects.Length; i++)
        {
            float angle = 360f / skillObjects.Length * i * Mathf.Deg2Rad;
            Vector3 pos = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;
            Gizmos.DrawWireSphere(pos, 0.1f);
        }
    }
}
