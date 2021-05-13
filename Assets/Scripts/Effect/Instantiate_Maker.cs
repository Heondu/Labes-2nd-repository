using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_Maker : MonoBehaviour
{
    [SerializeField]
    private float yaxisModifier = -0.5f;
    [SerializeField]
    private float intervalTime = 0.5f;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private AudioClip sound;
    [SerializeField]
    private bool onlyMove = false; 

    private Animator anim;

    private Vector2 v2;

    private float realTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        realTime += Time.deltaTime;
        if (onlyMove)
        {
            if (realTime >= intervalTime && anim.GetBool("Move"))
            {
                prefabCreator();
            }
        }
        else
        {
            if (realTime >= intervalTime)
            {
                prefabCreator();
            }
        }
    }

    private void prefabCreator()
    {
        realTime = 0;
        if (prefab)
            Instantiate(prefab, new Vector3(transform.position.x, transform.position.y + yaxisModifier, transform.position.z), Quaternion.identity);
        if (sound)
            SoundEffectManager.SoundEffect(sound);
    }
}
