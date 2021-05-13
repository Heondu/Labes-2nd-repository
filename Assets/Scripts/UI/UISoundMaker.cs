using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundMaker : MonoBehaviour
{
    [SerializeField]
    private AudioClip clickSound;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SoundEffectManager.SoundEffect(clickSound);
        }
    }
}
