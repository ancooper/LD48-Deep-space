using UnityEngine;

namespace ancoopergames
{
  public class InterractablePanel : MonoBehaviour
  {
    public Color NormalColor;
    public Color HilightColor;
    public ShowHide BigPanel;
    public GameObject Hilight;
    private bool near;
    public bool Near
    {
      get { return near; }
      set
      {
        near = value;
        if (Hilight != null)
          Hilight.SetActive(near);
        else
          spriteRenderer.color = near ? HilightColor : NormalColor;
      }
    }
    private SpriteRenderer spriteRenderer;
    void Start()
    {
      spriteRenderer = GetComponent<SpriteRenderer>();
      if (Hilight != null)
        Hilight.SetActive(false);
    }
  }
}