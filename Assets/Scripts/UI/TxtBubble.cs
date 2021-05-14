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

    private void Start()
    {
        tmpro = tmpGameObject.GetComponent<TextMeshProUGUI>();
        ChangeValue(faceSprite, str, f);
        anim = GetComponent<Animator>();
        PlaySoundEffect();
    }

    private void OnEnable()
    {
        StartCoroutine("KeyInputCheck");
    }

    private IEnumerator KeyInputCheck()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (Input.anyKey)
            {
                anim.SetTrigger("DieTrigger");
            }
            yield return null;
        }
    }

    //private void Update()
    //{
    //    if (Input.anyKey)
    //    {
    //        anim.SetTrigger("DieTrigger");
    //    }
    //}

    public void WriteText()
    {
        img.sprite = faceSprite;
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
        //Destroy(gameObject);
        StopCoroutine("KeyInputCheck");
        PlayerInput.instance.SetInputMode(InputMode.normal);
        gameObject.SetActive(false);
    }

    public void PlaySoundEffect()
    {
        if (ac)
            SoundEffectManager.SoundEffect(ac);
    }
}
