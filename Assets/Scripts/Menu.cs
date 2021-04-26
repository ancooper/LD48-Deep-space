using System.Collections;
using UnityEngine;

namespace ancoopergames
{
  public class Menu : MonoBehaviour
  {
    public GameObject NextSceneObject;
    private bool fadeOut = false;

    void Start(){
      Fade.Instance.FadeIn(0.5f);
    }
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space) && !fadeOut)
      {
        fadeOut = true;
        Fade.Instance.FadeOut(0.5f);
        Invoke("NextScene", 0.5f);
      }
    }

    private void NextScene()
    {
      gameObject.SetActive(false);
      NextSceneObject.SetActive(true);
    }
  }
}