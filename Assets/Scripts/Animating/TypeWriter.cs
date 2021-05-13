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
        tw.coroutine = tw.StartCoroutine(tw.Typewriter(s, delay));
    }

    public static void Write()
    {
        tw.coroutine = tw.StartCoroutine(tw.Typewriter(tw.sss, 0.1f));
    }

    private IEnumerator Typewriter(string s, float delay)
    {
        string s2 = "";
        for (int index = 0; index < s.Length; index++)
        {
            txt.text = s2;
            s2 += s[index];

            SoundEffectManager.SoundEffect("TypeWriter"); //Ÿ���� ġ�� ȿ����

            yield return WaitForRealSeconds(delay);
        }

        txt.text = s;

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
