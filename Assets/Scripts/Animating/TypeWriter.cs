using System.Collections;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    private Coroutine coroutine;
    private TextMeshProUGUI txt;
    private string sss;
    static TypeWriter _tw;

    public static TypeWriter tw
    {
        get
        {
            if (!_tw) //ȣ��ƴµ� SE�Ŵ����� ���� �� �����ϴ� ����
            {
                GameObject tw_Manager = new GameObject("TypeWrite_manager");
                _tw = tw_Manager.AddComponent(typeof(TypeWriter)) as TypeWriter;

                DontDestroyOnLoad(tw_Manager);
            }

            return _tw;
        }
    } //�̱��� �������� �׻�����

    /// <summary>
    /// �ѱ��ھ� Ÿ�����ϴ� �ִϸ��̼������� ���
    /// </summary>
    /// <param name="tmp">tmp</param>
    /// <param name="s">text</param>
    /// <param name="delay">delay</param>
    public static void Write(TextMeshProUGUI tmp, string s, float delay)
    {
        tw.txt = tmp;
        tw.sss = s;
        tw.coroutine = tw.StartCoroutine(tw.Typewriter(delay));
    }

    public static void Write()
    {
        tw.coroutine = tw.StartCoroutine(tw.Typewriter(0.1f));
    }

    public static void FastWrite()
    {
        tw.FinishCoroutine();
        tw.txt.text = tw.sss; 
    }

    private IEnumerator Typewriter(float delay)
    {
        string s2 = "";
        for (int index = 0; index < tw.sss.Length; index++)
        {
            txt.text = s2;
            s2 += tw.sss[index];

            SoundEffectManager.SoundEffect("TypeWriter"); //Ÿ���� ġ�� ȿ����

            yield return WaitForRealSeconds(delay);
        }

        txt.text = tw.sss;

        //yield return WaitForRealSeconds(1.5f);
        FinishCoroutine();
    }

    private void FinishCoroutine()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private Coroutine WaitForRealSeconds(float time)
    {
        return StartCoroutine(_WaitForRealSeconds(time));
    }
    private IEnumerator _WaitForRealSeconds(float time)
    {
        while (time > 0f)
        {
            time -= Mathf.Clamp(Time.unscaledDeltaTime, 0, 0.2f);
            yield return null;
        }
    }
}
