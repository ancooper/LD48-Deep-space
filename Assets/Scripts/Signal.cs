using System;
using UnityEngine;

namespace ancoopergames
{
  public class Signal : MonoBehaviour
  {
    public Sprite[] SignalSprites;
    private float signal;
    private SpriteRenderer spriteRenderer;
    public int value;

    public float Value
    {
      get { return signal; }
      set { signal = value; 
        ChangeSignal(signal);
      }
    }
    void Start(){
      spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void ChangeSignal(float signal)
    {
      if (signal > 0.8f) spriteRenderer.sprite = SignalSprites[0];
      else if (signal > 0.6f) spriteRenderer.sprite = SignalSprites[1];
      else if (signal > 0.4f) spriteRenderer.sprite = SignalSprites[2];
      else if (signal > 0.2f) spriteRenderer.sprite = SignalSprites[3];
      else if (signal > Mathf.Epsilon) spriteRenderer.sprite = SignalSprites[4];
      else spriteRenderer.sprite = SignalSprites[5];

      if (signal > 0.8f) value = 0;
      else if(signal > 0.4f) value = 3;
      else if(signal > Mathf.Epsilon) value = 4;
      else value = 5;
    }    
  }
}