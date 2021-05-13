using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] AudioClip aud;
    private void Start()
    {
        if(aud)
            MusicManager.MusicPlay(aud);
    }
}
