using UnityEngine;

namespace ancoopergames
{
  public class NavGame : MonoBehaviour
  {
    public Transform Antenna;
    public Signal AntennaSignal;
    public Color GreenColor;
    public Color YellowColor;
    public Color RedColor;
    public Color LostSignalColor;

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
          spriteRenderer.color = GreenColor;
          break;
        case NavState.YELLOW:
          spriteRenderer.color = YellowColor;
          break;
        case NavState.RED:
          spriteRenderer.color = RedColor;
          break;
        case NavState.LOSTSIGNAL:
          spriteRenderer.color = LostSignalColor;
          break;
      }
    }

    private Vector2 offset;
    private SpriteRenderer spriteRenderer;
    private float stability;

    public Vector2 Offset
    {
      get { return offset; }
      set
      {
        if (State == NavState.LOSTSIGNAL) return;
        offset = value;
        if (offset.sqrMagnitude > 0.2f) offset = offset.normalized * Mathf.Sqrt(0.2f);
        if (offset.sqrMagnitude < 0.025f)
          State = NavState.GREEN;
        else if (offset.sqrMagnitude < 0.075f)
          State = NavState.YELLOW;
        else
          State = NavState.RED;



        Antenna.transform.localPosition = (Vector3)offset;
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
      if (stability > deltaTime){
        stability -= deltaTime;
        driftVector -= Offset.normalized * 0.03f * deltaTime;
      }else
        driftVector += Offset.normalized * 0.01f * deltaTime;
      Offset += driftVector;
    }
  }
}