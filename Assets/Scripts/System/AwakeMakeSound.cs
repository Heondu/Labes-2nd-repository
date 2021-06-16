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
            MakeSoundEffect(ac);
        }
    }

    public void MakeSoundEffect(AudioClip _ac)
    {
        SoundEffectManager.SoundEffect(_ac);
        Debug.Log("12");
    }
}
