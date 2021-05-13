using UnityEngine;

public class Rotation : MonoBehaviour
{
    public static void Rotate(Transform target, Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        target.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public static void Rotate(Transform target, Vector3 dir, Vector3 center)
    {
        Rotate(target, dir);
        float angle = Mathf.Atan2(dir.y, dir.x);
        float distance = Vector3.Distance(center, target.position);
        float x = distance * Mathf.Cos(angle);
        float y = distance * Mathf.Sin(angle);
        target.localPosition = new Vector3(x, y, 0);
    }

    public static float GetAngle(Vector3 from, Vector3 to)
    {
        Vector2 dir = (to - from).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return angle - 90;
    }

    public static float GetAngle(Vector2 dir)
    {
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
    }
}
