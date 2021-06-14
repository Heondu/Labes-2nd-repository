using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LazyCamera : MonoBehaviour
{
    public static LazyCamera instance;
    [SerializeField]
    private Transform target;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.3f;
    private float originSize;
    private Camera cam;

    [SerializeField]
    private float range = 4;

    private MapData mapData = null;
    public UnityEvent onMapDataChanged = new UnityEvent();

    private float width;
    private float height;

    [SerializeField]
    private bool isScreenLock = true;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        cam = GetComponent<Camera>();
        originSize = Camera.main.orthographicSize;
        height = 2 * originSize;
        width = height * cam.aspect;
    }

    void Update()
    {
        if (!target)
        {
            target = GameObject.Find("Player").transform;
        }

        UpdateCamera();
    }

    /// <summary>
    /// 마우스와 플레이어 사이 중간점 구하기
    /// </summary>
    Vector3 MiddleOfMouseAndPlayer
    {
        get
        {
            Vector3 v3 = cam.ScreenToWorldPoint(Input.mousePosition);
            v3 = new Vector3((target.position.x + v3.x) / 2, (target.position.y + v3.y) / 2, -10);
            return new Vector3(Mathf.Clamp(v3.x, target.position.x - range, target.position.x + range), Mathf.Clamp(v3.y, target.position.y - range, target.position.y + range), target.position.z - 300);
        }
    }

    public void SetMapData(MapData mapData)
    {
        this.mapData = mapData;
        onMapDataChanged.Invoke();
    }

    public MapData GetMapData()
    {
        return mapData;
    }

    private Vector3 GetClampedSizeOnScreen()
    {
        Vector3 middlePos = MiddleOfMouseAndPlayer;

        if (isScreenLock && mapData != null)
        {
            float limitX = (mapData.Size.x - width) / 2;
            float limitY = (mapData.Size.y - height) / 2;

            middlePos.x = Mathf.Clamp(middlePos.x, -limitX + mapData.Position.x, limitX + mapData.Position.x);
            middlePos.y = Mathf.Clamp(middlePos.y, -limitY + mapData.Position.y, limitY + mapData.Position.y);
        }

        return middlePos;
    }

    private void UpdateCamera()
    {
        //transform.position = Vector3.SmoothDamp(transform.position, MiddleOfMouseAndPlayer, ref velocity, smoothTime);
        transform.position = Vector3.SmoothDamp(transform.position, GetClampedSizeOnScreen(), ref velocity, smoothTime);
    }

    public IEnumerator ZoomIn(float amount, float duration)
    {
        float time = 0;
        while (time < 1)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originSize / amount, time);
            time += Time.deltaTime;
            yield return null;
        }
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        while (time < 1)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originSize, time);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator ZoomOut(float amount, float duration)
    {
        float time = 0;
        while (time < 1)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originSize * amount, time);
            time += Time.deltaTime;
            yield return null;
        }
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        while (time < 1)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originSize, time);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void ChangeTarget(Transform target)
    {
        this.target = target;
    }

    public float GetWidth()
    {
        return width;
    }
}
