using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject bubble1;
    [SerializeField]
    private GameObject bubble2;

    public void Bubble1Pop()
    {
        bubble1.SetActive(true);
    }
    public void Bubble2Pop()
    {
        bubble2.SetActive(true);
    }
    public void Bubble1Die()
    {
        bubble1.GetComponent<Animator>().SetTrigger("DieTrigger");
    }
    public void Bubble2Die()
    {
        bubble2.GetComponent<Animator>().SetTrigger("DieTrigger");
    }
    public void LoadMain()
    {
        LoadingSceneManager.LoadScene("MainScene");
    }
}
