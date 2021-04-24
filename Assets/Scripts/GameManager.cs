using System.Collections;
using UnityEngine;

namespace ancoopergames
{
  public class GameManager : MonoBehaviour
  {
    private State state;

    public static GameManager Instance { get; private set; }
    public enum State { SPLASHSCREEN, MENU, LOADLEVEL, CONTRAGULATION, GAMEOVER };


    void Start()
    {
      Instance = this;
    }

    void BeginNewState(State newState)
    {
      state = newState;
      switch (state)
      {
        case State.SPLASHSCREEN:
          break;
        case State.MENU:
          break;
        case State.LOADLEVEL:
          break;
        case State.CONTRAGULATION:
          break;
        case State.GAMEOVER:
          break;
      }
    }

    void EndState()
    {
      switch (state)
      {
        case State.SPLASHSCREEN:
          break;
        case State.MENU:
          break;
        case State.LOADLEVEL:
          break;
        case State.CONTRAGULATION:
          break;
        case State.GAMEOVER:
          break;
      }
    }

    void SwitchState(State newState, float delay = 0f)
    {
      StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState, float delay)
    {
      EndState();
      yield return new WaitForSeconds(delay);
      BeginNewState(newState);
    }

    void Update()
    {

    }
  }
}