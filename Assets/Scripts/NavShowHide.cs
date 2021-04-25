using UnityEngine;

namespace ancoopergames
{
  public class NavShowHide : ShowHide
  {
    public float ShowX;
    public float HideX;
    private float value;
    private float delta;

    public override void Show()
    {
      delta = 10f;
    }

    public override void Hide()
    {
      delta = -10f;
    }

    void Start()
    {
      value = 0f;
      SetX(HideX);
    }

    private void SetX(float value)
    {
      var position = transform.position;
      position.x = value;
      transform.position = position;
    }

    void Update(){
      value = Mathf.Clamp01(value + delta * Time.deltaTime);
      SetX(Mathf.Lerp(HideX, ShowX, value));      
    }
  }
}