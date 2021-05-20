using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TxtBubble : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private GameObject tmpGameObject;
    [SerializeField] private AudioClip ac;
    private float _interval;
    private Animator anim;


    //이 부분은  dialogue csv 파일의 dialogue001 등으로 대체됨
    [SerializeField] private Sprite faceSprite;
    [SerializeField] private string str;
    [SerializeField] private float f = 0.2f;

    private TextMeshProUGUI tmpro;

    public delegate void Callback();
    private Callback callback = null;
    private bool isFinish = false;

    private void Awake()
    {
        tmpro = tmpGameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        PlaySoundEffect();
    }

    public void Init()
    {
        gameObject.SetActive(true);
        tmpro.text = "";
        StartCoroutine("KeyInputCheck");
    }

    private IEnumerator KeyInputCheck()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            if (Input.anyKeyDown)
            {
                if (isFinish)
                {
                    isFinish = false;
                    callback();
                }
                else
                {
                    TypeWriter.FastWrite();
                }
            }
            yield return null;
        }
    }

    private void IsFinish()
    {
        isFinish = true;
    }

    public void WriteText()
    {
        TypeWriter.SetCallBack(IsFinish);
        TypeWriter.Write(tmpro, str, f);
    }

    public void ChangeValue(Sprite face, string changeStr, float interval)
    {
        img.sprite = face;
        str = changeStr;
        _interval = interval;
    }

    public void Die()
    {
        PlaySoundEffect();
        StopCoroutine("KeyInputCheck");
        gameObject.SetActive(false);
    }

    public void SetDieTrigger()
    {
        anim.SetTrigger("DieTrigger");
    }

    public void PlaySoundEffect()
    {
        if (ac)
            SoundEffectManager.SoundEffect(ac);
    }

    public void SetCallback(Callback call)
    {
        callback = call;
    }
}
