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

    private void Awake()
    {
        tmpro = tmpGameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        PlaySoundEffect();
    }

    public void Init(List<Dialogue> dialogues)
    {
        gameObject.SetActive(true);
        tmpro.text = "";
        ChangeValue(dialogues[0].face, dialogues[0].content, dialogues[0].interval);
        StartCoroutine("KeyInputCheck", dialogues);
    }

    private IEnumerator KeyInputCheck(List<Dialogue> dialogues)
    {
        yield return new WaitForSeconds(0.5f);

        int index = 1; 
        int keyCount = 0;
        while (true)
        {
            if (Input.anyKeyDown)
            {
                keyCount++;
                TypeWriter.FastWrite();
                if (keyCount >= 2)
                {
                    if (index < dialogues.Count)
                    {
                        ChangeValue(dialogues[index].face, dialogues[index].content, dialogues[index].interval);
                        index++;
                        keyCount = 0;
                        WriteText();
                    }
                    else
                    {
                        anim.SetTrigger("DieTrigger");
                    }
                }
            }
            yield return null;
        }
    }

    public void WriteText()
    {
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
        PlayerInput.instance.SetInputMode(InputMode.normal);
        gameObject.SetActive(false);
    }

    public void PlaySoundEffect()
    {
        if (ac)
            SoundEffectManager.SoundEffect(ac);
    }
}
