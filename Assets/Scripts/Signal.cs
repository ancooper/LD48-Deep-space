using System;
using UnityEngine;

namespace ancoopergames
{
  public class Signal : MonoBehaviour
  {
    private float signal;
    public float Value
    {
      get { return signal; }
      set { signal = value; 
        ChangeSignal(signal);
      }
    }

    private void ChangeSignal(float signal)
    {
      var enableColor = GreenColor;
      if(signal < 0.66f && signal >= 0.33) enableColor = YellowColor;
      if(signal < 0.33) enableColor = RedColor;

      Bars[4].color = signal>0.8f ? enableColor : DisableColor;
      Bars[3].color = signal>0.6f ? enableColor : DisableColor;
      Bars[2].color = signal>0.4f ? enableColor : DisableColor;
      Bars[1].color = signal>0.2f ? enableColor : DisableColor;
      Bars[0].color = signal>Mathf.Epsilon ? enableColor : DisableColor;
    }

    public Color GreenColor;
    public Color YellowColor;
    public Color RedColor;
    public Color DisableColor;
    public SpriteRenderer[] Bars;
    
  }
}