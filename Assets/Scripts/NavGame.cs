using UnityEngine;

namespace ancoopergames
{
  public class NavGame : MonoBehaviour
  {
    public Transform Antenna;
    public Signal AntennaSignal;
    public Sprite[] AntennaSprites;
    public int Value = 0;
    public enum NavState { GREEN, YELLOW, RED, LOSTSIGNAL }
    private NavState state;
    public NavState State
    {
      get { return state; }
      set
      {
        state = value;
        ChangeState(state);
      }
    }
    private float signal = 1f;
    public float Signal
    {
      get { return signal; }
      set
      {
        signal = Mathf.Clamp01(value);
        ChangeSignal(signal);
      }
    }

    private void ChangeSignal(float signal)
    {
      AntennaSignal.Value = signal;
    }

    void Start()
    {
      spriteRenderer = Antenna.GetComponent<SpriteRenderer>();
      stability = 5f;
    }
    private void ChangeState(NavState state)
    {
      switch (state)
      {
        case NavState.GREEN:
          spriteRenderer.sprite = AntennaSprites[0];
          break;
        case NavState.YELLOW:
          spriteRenderer.sprite = AntennaSprites[1];
          break;
        case NavState.RED:
          spriteRenderer.sprite = AntennaSprites[2];
          break;
        case NavState.LOSTSIGNAL:
          spriteRenderer.sprite = AntennaSprites[3];
          Value = 5;
          GameOver();
          break;
      }
    }

    private void GameOver()
    {
      Level.Instance.Player.GameOver(GameOverTitle.SG);
    }

    private Vector2 offset;
    private SpriteRenderer spriteRenderer;
    private float stability;

    public Vector2 Offset
    {
      get { return offset; }
      set
      {
        var sigValue = AntennaSignal.value;
        if (State == NavState.LOSTSIGNAL)
          return;
        offset = value;
        if (offset.sqrMagnitude > 0.2f) offset = offset.normalized * Mathf.Sqrt(0.2f);
        if (offset.sqrMagnitude < 0.025f)
        {
          State = NavState.GREEN;
          Value = Mathf.Max(0, sigValue);
        }
        else if (offset.sqrMagnitude < 0.075f)
        {
          State = NavState.YELLOW;
          Value = Mathf.Max(1, sigValue);
        }
        else
        {
          State = NavState.RED;
          Value = Mathf.Max(2, sigValue);
        }

        Antenna.transform.localPosition = (Vector3)offset * 2.65f;
      }
    }

    private void Stabilising()
    {
      if (state == NavState.GREEN)
        stability = 3f;
    }

    internal void Left()
    {
      var offset = Offset;
      offset.x -= 0.025f;
      Offset = offset;
      Stabilising();
    }

    internal void Right()
    {
      var offset = Offset;
      offset.x += 0.025f;
      Offset = offset;
      Stabilising();
    }

    internal void Up()
    {
      var offset = Offset;
      offset.y += 0.025f;
      Offset = offset;
      Stabilising();
    }

    internal void Down()
    {
      var offset = Offset;
      offset.y -= 0.025f;
      Offset = offset;
      Stabilising();
    }

    void Update()
    {
      if (state == NavState.GREEN)
        Signal += 0.3f * Time.deltaTime;
      if (state == NavState.RED)
        Signal -= 0.1f * Time.deltaTime;

      if (Signal < Mathf.Epsilon)
        State = NavState.LOSTSIGNAL;

      if (state != NavState.LOSTSIGNAL)
        Drift(Time.deltaTime, 0.4f);
    }

    private void Drift(float deltaTime, float amount = 1f)
    {
      var driftVector = new Vector2(Random.Range(-amount, amount), Random.Range(-amount, amount)) * deltaTime;
      if (stability > deltaTime)
      {
        stability -= deltaTime;
        driftVector -= Offset.normalized * 0.03f * deltaTime;
      }
      else
        driftVector += Offset.normalized * 0.01f * deltaTime;
      Offset += driftVector;
    }
  }
}