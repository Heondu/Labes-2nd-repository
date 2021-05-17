using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 startingPos = Vector3.zero;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();

        player.transform.position = startingPos;
    }
}
