using UnityEngine;

public class MapData : MonoBehaviour
{
    [SerializeField]
    private Vector2 size = Vector2.zero;
    public Vector2 Size => size;

    [SerializeField]
    private Vector2 position = Vector2.zero;
    public Vector2 Position => position;

    [SerializeField] private Vector2 startingPos;

    [SerializeField] private bool useTransformSize;
    [SerializeField] private bool useTransformPosition;
    [SerializeField] private bool useColliderSize;
    [SerializeField] private bool autoAssignMapData;

    [SerializeField] private string mapId;

    private void Start()
    {
        if (useTransformSize)
            size = transform.localScale;
        else if (useColliderSize)
        {
            Bounds bounds = GetComponent<Collider2D>().bounds;
            size = bounds.size;
            position = bounds.center;
        }
        else if (useTransformPosition)
            position = transform.position;

        if (autoAssignMapData)
        {
            LazyCamera.instance.SetMapData(this);
            FindObjectOfType<Player>().transform.position = startingPos;
            LazyCamera.instance.transform.position = startingPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LazyCamera.instance.SetMapData(this);
            collision.GetComponent<Player>().SetMapData(this);
        }
        else if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
                 enemy.SetMapData(this);
        }
    }

    public void ClampPos(Transform target)
    {
        float limitX = size.x / 2;
        float limitY = size.y / 2;

        float x = Mathf.Clamp(target.position.x, -limitX + position.x, limitX + position.x);
        float y = Mathf.Clamp(target.position.y, -limitY + position.y, limitY + position.y);

        target.position = new Vector3(x, y, target.position.z);
    }

    public string GetMapId()
    {
        return mapId;
    }
}
