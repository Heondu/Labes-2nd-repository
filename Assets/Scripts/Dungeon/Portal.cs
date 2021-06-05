using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private enum Coord { World = 0, Local };

    [SerializeField]
    private Portal connectedPortal;

    [SerializeField]
    private Vector3 spawnPos = Vector3.zero;
    [SerializeField]
    private Coord coord = Coord.Local;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PortalCollider"))
        {
            collision.GetComponentInParent<Player>().SetMapData(null);
            connectedPortal.Execute(collision.transform.parent);
        }
    }

    public void Execute(Transform target)
    {
        Vector3 newPos;
        if (coord == Coord.World)
            newPos = spawnPos;
        else
        {
            newPos = transform.position + spawnPos;
        }

        target.position = newPos;
    }
}
