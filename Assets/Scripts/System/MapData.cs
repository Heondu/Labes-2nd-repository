using UnityEngine;

public class MapData : MonoBehaviour
{
    [SerializeField]
    private Vector2 size = Vector2.zero;
    public Vector2 Size => size;
    [SerializeField]
    private Vector2 position = Vector2.zero;
    public Vector2 Position => position;
    [SerializeField]
    private bool autoSize = true;
    [SerializeField]
    private bool autoPosition = true;
    [SerializeField]
    private bool autoAssignMapData = true;

    private float width;
    private float height;

    private void Start()
    {
        if (autoSize)
            size = transform.localScale;

        if (autoPosition)
            position = transform.position;

        if (autoAssignMapData)
            LazyCamera.instance.SetMapData(this);

        height = 2 * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
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
            collision.GetComponent<Enemy>().SetMapData(this);
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
}
