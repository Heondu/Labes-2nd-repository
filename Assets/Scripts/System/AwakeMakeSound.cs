using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeMakeSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip ac;

    private void Awake()
    {
        if (ac)
        {
            SoundEffectManager.SoundEffect(ac);
        }
    }
}
