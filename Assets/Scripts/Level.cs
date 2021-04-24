using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ancoopergames
{
  public class Level : MonoBehaviour
  {
    public Player Player;
    public InterractablePanel NavigationPanel;
    public InterractablePanel NuclearPanel;
    public InterractablePanel ChargePanel;
    public NavGame NavGame;
    public ChargeGame ChargeGame;
    public NuclearGame NuclearGame;
    public static Level Instance;
    void Start()
    {
      Instance = GetComponent<Level>();
    }
  }
}