using System.Collections;
using UnityEngine;

namespace ancoopergames
{
  public class Fade : MonoBehaviour
  {
    public GameObject Face;
    public static Fade Instance;

    void Start()
    {
      Instance = this;
    }
    public void FadeOut(float time)
    {
      StartCoroutine(DoFade(0f, 1f, true, time));
    }
    public void FadeIn(float time)
    {
      StartCoroutine(DoFade(1f, 0f, false, time));
    }
    private IEnumerator DoFade(float a, float b, bool show, float time)
    {
      if (show) Face.SetActive(true);
      SetFaceAlpha(a);
      var endtime = Time.time + time;
      while (endtime - Time.time > 0)
      {
        var val = Mathf.Lerp(a, b, 1 - (endtime - Time.time) / time);
        SetFaceAlpha(val);
        yield return new WaitForSeconds(0.02f);
      }
      SetFaceAlpha(b);
      if (!show) Face.SetActive(false);
    }

    private void SetFaceAlpha(float a)
    {
      var facecolor = Face.GetComponent<SpriteRenderer>().color;
      facecolor.a = a;
      Face.GetComponent<SpriteRenderer>().color = facecolor;
    }
  }
}