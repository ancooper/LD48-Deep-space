using UnityEngine;

namespace ancoopergames
{
  public class NuclearGame : MonoBehaviour
  {
    public Transform Bar;
    public Color GreenColor;
    public Color BlueColor;
    public Color RedColor;

    void Start()
    {
      spriteRenderer = Bar.GetComponent<SpriteRenderer>();
      stability = 5f;
    }
    private void ChangeTemp()
    {
      var position = Bar.transform.localPosition;
      position.y = Offset * 2.65f;
      Bar.transform.localPosition = position;

      if (Mathf.Abs(Offset) < 0.15f)
        spriteRenderer.color = GreenColor;
      else if (Offset < 0)
        spriteRenderer.color = BlueColor;
      else
        spriteRenderer.color = RedColor;
    }

    private float offset;
    private SpriteRenderer spriteRenderer;
    private float stability;

    public float Offset
    {
      get { return offset; }
      set
      {
        offset = Mathf.Clamp(value, -0.43f, 0.43f);
        ChangeTemp();
      }
    }

    private void Stabilising()
    {
      if (Mathf.Abs(offset) < 0.5f)
        stability = 3f;
    }

    internal void Up()
    {
      Offset += 0.025f;
      Stabilising();
    }

    internal void Down()
    {
      Offset -= 0.025f;
      Stabilising();
    }

    void Update()
    {
      Drift(Time.deltaTime, 0.4f);
    }

    private void Drift(float deltaTime, float amount = 1f)
    {
      var driftValue = Random.Range(-amount, amount) * deltaTime;
      if (stability > deltaTime)
      {
        stability -= deltaTime;
        driftValue -= Mathf.Sign(Offset) * 0.03f * deltaTime;
      }
      else
        driftValue += Mathf.Sign(Offset) * 0.01f * deltaTime;
      Offset += driftValue;
    }
  }
}