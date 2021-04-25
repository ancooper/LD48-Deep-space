using UnityEngine;

namespace ancoopergames
{
  public class ShowHide : MonoBehaviour
  {
    public virtual void Show()
    {
      Debug.Log("Show");

    }

    public virtual void Hide()
    {
      Debug.Log("Hide");

    }
  }
}