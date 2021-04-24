using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ancoopergames
{
  public class Player : MonoBehaviour
  {
    private Rigidbody2D body;
    private PlayerState state;
    private Vector3 speed = Vector3.right * 2f;
    private float dashVelocity;
    private float dashTime = 0f;
    private int batteryValue;
    public int Battery {
      get {return batteryValue;}
      set {batteryValue = Mathf.Clamp(value, 0, 5);
        Level.Instance.ChargeGame.Value = batteryValue;
      }
    }
    private int dashPower = 1;

    public enum PlayerState { MOVING, CHARDGING, REACTOR, NAVIGATION };

    void Start()
    {
      body = GetComponent<Rigidbody2D>();
      state = PlayerState.MOVING;
      Battery = 5;
    }
    void Update()
    {
      switch (state)
      {
        case PlayerState.MOVING:
          Moving();
          break;
        case PlayerState.NAVIGATION:
          Navigating();
          break;
        case PlayerState.CHARDGING:
          Charging();
          break;
        case PlayerState.REACTOR:
          Termocontrolling();
          break;
      }

    }

    private void Termocontrolling()
    {
      if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        Level.Instance.NuclearGame.Up();
      if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        Level.Instance.NuclearGame.Down();
      if (Input.GetKeyDown(KeyCode.E))
        Move();
    }

    private void Charging()
    {
      if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        Battery++;
      if (Input.GetKeyDown(KeyCode.E))
        Move();
    }

    private void Navigating()
    {
      if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        Level.Instance.NavGame.Left();
      if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        Level.Instance.NavGame.Right();
      if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        Level.Instance.NavGame.Up();
      if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        Level.Instance.NavGame.Down();
      if (Input.GetKeyDown(KeyCode.E))
        Move();
    }

    private void Move()
    {
      state = PlayerState.MOVING;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
      if (other.gameObject == Level.Instance.NavigationPanel.gameObject)
        Level.Instance.NavigationPanel.Near = true;
      if (other.gameObject == Level.Instance.NuclearPanel.gameObject)
        Level.Instance.NuclearPanel.Near = true;
      if (other.gameObject == Level.Instance.ChargePanel.gameObject)
        Level.Instance.ChargePanel.Near = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
      if (other.gameObject == Level.Instance.NavigationPanel.gameObject)
        Level.Instance.NavigationPanel.Near = false;
      if (other.gameObject == Level.Instance.NuclearPanel.gameObject)
        Level.Instance.NuclearPanel.Near = false;
      if (other.gameObject == Level.Instance.ChargePanel.gameObject)
        Level.Instance.ChargePanel.Near = false;
    }

    private void Moving()
    {
      var velocity = 0f;
      if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) && dashTime > Mathf.Epsilon)
        velocity--;
      if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && dashTime > Mathf.Epsilon)
        velocity++;
      if (Input.GetKeyDown(KeyCode.Space) && velocity != 0 && dashTime < Mathf.Epsilon && Battery >= dashPower)
      {
        dashVelocity = velocity * 3f;
        dashTime = 0.2f;
        Battery -= dashPower;
        //Discharge battery
      }

      if (Input.GetKeyDown(KeyCode.E))
        Interract();

      if (dashTime > Mathf.Epsilon)
      {
        dashTime -= Time.deltaTime;
        body.velocity = speed * dashVelocity;
      }
      else
      {
        body.velocity = speed * velocity;
      }
    }

    private void Interract()
    {
      if (Level.Instance.NavigationPanel.Near)
        state = PlayerState.NAVIGATION;
      if (Level.Instance.ChargePanel.Near)
        state = PlayerState.CHARDGING;
      if (Level.Instance.NuclearPanel.Near)
        state = PlayerState.REACTOR;
    }
  }
}