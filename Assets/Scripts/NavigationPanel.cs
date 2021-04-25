using UnityEngine;

namespace ancoopergames
{
  public class NavigationPanel : MonoBehaviour
  {
    public NavGame NavGame;

    public Sprite[] Sprites;
    private SpriteRenderer spriteRenderer;

    void Start(){
      spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
      spriteRenderer.sprite = Sprites[NavGame.Value];
    }
  }
}