using UnityEngine;
using UnityEngine.SceneManagement;

namespace ancoopergames
{
  public enum GameOverTitle { OH, CD, SG };
  public class GameOver : MonoBehaviour
  {
    public GameObject Menu;

    public GameObject Overheated;
    public GameObject CooledDown;
    public GameObject SignalIsGone;

    private bool fadeOut = false;

    void Start()
    {
      Fade.Instance.FadeIn(0.5f);
    }
    public void SetGameOver(GameOverTitle title)
    {
      Overheated.SetActive(title == GameOverTitle.OH);
      CooledDown.SetActive(title == GameOverTitle.CD);
      SignalIsGone.SetActive(title == GameOverTitle.SG);
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
      //Menu.SetActive(true);
    }
  }
}