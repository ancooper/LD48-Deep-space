using UnityEngine;

namespace ancoopergames
{
  public class InterractablePanel : MonoBehaviour
  {
    public Color NormalColor;
    public Color HilightColor;
    private bool near;
    public bool Near
    {
      get { return near; }
      set
      {
        near = value;
        spriteRenderer.color = near ? HilightColor : NormalColor;
      }
    }
    private SpriteRenderer spriteRenderer;
    void Start()
    {
      spriteRenderer = GetComponent<SpriteRenderer>();
    }
  }
}