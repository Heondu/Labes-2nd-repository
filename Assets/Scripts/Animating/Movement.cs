using UnityEngine;

public class Movement : MonoBehaviour
{
    public void Execute(Vector3 direction, float speed, MapData mapData)
    {
        transform.position += direction * speed * Time.deltaTime;

        if (mapData != null)
        {
            mapData.ClampPos(transform);
        }
    }
}
