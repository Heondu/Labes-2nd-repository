using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [SerializeField]
    private Vector2 dir;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private bool useSkillSpeed = true;

    private void Update()
    {
        if (dir == Vector2.zero)
        {
            Move(transform.up, speed);
        }
        else
        {
            Move(dir, speed);
        }
    }

    public void Move(Vector3 direction, float speed)
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetDir(Vector2 dir)
    {
        this.dir = dir;
        float angle = Rotation.GetAngle(dir);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void SetDir(Vector2 dir, float guide)
    {
        float angle = Rotation.GetAngle(dir);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * guide);
        this.dir = transform.up;
    }

    public void SetSpeed(float speed)
    {
        if (useSkillSpeed)
            this.speed = speed;
    }
}
