using UnityEngine;
using UnityEngine.SceneManagement;

namespace ancoopergames
{
  public class Victory : MonoBehaviour
  {
    private bool fadeOut = false;

    void Start()
    {
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
      SceneManager.LoadScene("Main");
    }
  }
}