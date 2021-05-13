using System.Collections;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txt;
    private Coroutine coroutine;

    public static TypeWriter tw;

    string sss;

    private void Start()
    {
        tw = GetComponent<TypeWriter>();

        sss = "입력한다아아아아아\n123123123";

        Write();
    }

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

            yield return WaitForRealSeconds(delay);
        }

        txt.text = s;

        yield return WaitForRealSeconds(1.5f);
        txt.text = "";
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
