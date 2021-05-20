using System.Collections;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    private Coroutine coroutine;
    private TextMeshProUGUI txt;
    private string sss;
    static TypeWriter _tw;

    public delegate void Callback();
    private Callback callback = null;

    public static TypeWriter tw
    {
        get
        {
            if (!_tw) //호출됐는데 SE매니저가 없을 시 생성하는 과정
            {
                GameObject tw_Manager = new GameObject("TypeWrite_manager");
                _tw = tw_Manager.AddComponent(typeof(TypeWriter)) as TypeWriter;

                DontDestroyOnLoad(tw_Manager);
            }

            return _tw;
        }
    } //싱글톤 형식으로 항상유지

    /// <summary>
    /// 한글자씩 타이핑하는 애니메이션형식의 출력
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
        tw.callback();
    }

    private IEnumerator Typewriter(float delay)
    {
        string s2 = "";
        for (int index = 0; index < tw.sss.Length; index++)
        {
            txt.text = s2;
            s2 += tw.sss[index];

            SoundEffectManager.SoundEffect("TypeWriter"); //타이핑 치는 효과음

            yield return WaitForRealSeconds(delay);
        }

        txt.text = tw.sss;
        tw.callback();

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

    public static void SetCallBack(Callback call)
    {
        tw.callback = call;
    }
}
