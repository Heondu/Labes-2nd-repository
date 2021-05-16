using UnityEngine;

public enum SortingMethod { NULL = 0, Awake, Update }

public class Sorter : MonoBehaviour
{
    [SerializeField]
    private SortingMethod sortingMethod = SortingMethod.Awake;
    [SerializeField]
    private float offset;

    private void Awake()
    {
        if (sortingMethod != SortingMethod.NULL)
        {
            Sort();
        }
    }

    private void Update()
    {
        if (sortingMethod == SortingMethod.Update)
        {
            Sort();
        }
    }

    private void Sort()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y + offset);
    }

    //[SerializeField]
    //private TransparencySortMode transparencySortMode = TransparencySortMode.CustomAxis;
    //[SerializeField]
    //private Vector3 sort = new Vector3(0, 1, 1);
    //
    //private void Awake()
    //{
    //    Camera.main.transparencySortMode = transparencySortMode;
    //    Camera.main.transparencySortAxis = sort;
    //}
}
