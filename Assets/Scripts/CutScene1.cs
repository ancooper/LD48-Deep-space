using UnityEngine;

namespace ancoopergames
{
  public class CutScene1 : MonoBehaviour
  {
    public GameObject NextSceneObject;
    public void FadeIn()
    {
      Fade.Instance.FadeIn(0.5f);
    }
    public void FadeOut()
    {
      Fade.Instance.FadeOut(0.5f);
    }
    public void EndScene()
    {
      gameObject.SetActive(false);
      NextSceneObject.SetActive(true);
    }
  }
}