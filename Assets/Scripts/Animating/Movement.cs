using UnityEngine;

public class Movement : MonoBehaviour
{
    public void Execute(Vector3 direction, float speed)
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
