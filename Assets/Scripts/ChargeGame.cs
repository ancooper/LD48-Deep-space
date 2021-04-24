using System;
using UnityEngine;

namespace ancoopergames
{
  public class ChargeGame : MonoBehaviour
  {
    public Color FullColor;
    public Color EmptyColor;
    public SpriteRenderer[] Bars;
    private int battery;
    public int Value{
      get {return battery;}
      set {battery = Mathf.Clamp(value, 0, 5);
        Bars[4].color = battery > 0 ? FullColor : EmptyColor;
        Bars[3].color = battery > 1 ? FullColor : EmptyColor;
        Bars[2].color = battery > 2 ? FullColor : EmptyColor;
        Bars[1].color = battery > 3 ? FullColor : EmptyColor;
        Bars[0].color = battery > 4 ? FullColor : EmptyColor;
      }
    }
  }
}