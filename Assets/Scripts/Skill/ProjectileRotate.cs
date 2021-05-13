using UnityEngine;

public class ProjectileRotate : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 0;
    private float angle = 0;
    public bool repeat = false;

    private void Update()
    {
        if (repeat)
        {
            Rotate(angle);
            angle += rotateSpeed * Time.deltaTime;
            if (angle > 360) angle = 0;
            else if (angle < 0) angle = 360;
        }
    }

    public void Rotate(float angle)
    {
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Rotate(float angle, float guide)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * guide);
    }

    public void LookAt(Vector2 dir)
    {
        float angle = Rotation.GetAngle(dir);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void SetSpeed(float speed)
    {
        rotateSpeed = speed;
    }

    public float GetSign()
    {
        return Mathf.Sign(rotateSpeed);
    }
}
