using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField]
    private GameObject minimapIconPrefab;
    [SerializeField]
    private Vector2 size;
    [SerializeField]
    private Vector2 offset;
    [SerializeField]
    private bool autoSize = false;
    [SerializeField]
    private bool usePrefabSize = false;
    [SerializeField]
    private float sortZ;
    private GameObject minimapIcon;

    private void Update()
    {
        if (minimapIcon == null) Setup();

        minimapIcon.transform.position = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, sortZ);
    }

    private void OnEnable()
    {
        if (minimapIcon != null)
            minimapIcon.SetActive(true);
    }

    private void OnDisable()
    {
        if (minimapIcon != null)
            minimapIcon.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(offset.x, offset.y, 0), size);
    }

    private void Setup()
    {
        GameObject minimapHolder = GameObject.Find("MinimapHolder");

        if (minimapHolder == null)
        {
            minimapHolder = new GameObject("MinimapHolder");
        }

        minimapIcon = Instantiate(minimapIconPrefab, new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, sortZ), Quaternion.identity, minimapHolder.transform);

        if (autoSize)
        {
            Minimap minimap = null;
            if (transform.parent != null)
            {
                minimap = transform.parent.GetComponentInParent<Minimap>();
            }
            if (minimap != null && minimap != this)
            {
                size = new Vector3(transform.localScale.x * minimap.transform.localScale.x, transform.localScale.y * minimap.transform.localScale.y);
            }
            else
            {
                size = transform.localScale;
            }
        }
        else if (usePrefabSize)
        {
            size = minimapIcon.transform.localScale;
        }

        minimapIcon.transform.localScale = size;
    }
}
