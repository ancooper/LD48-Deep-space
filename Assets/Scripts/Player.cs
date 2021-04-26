using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ancoopergames
{
  public class Player : MonoBehaviour
  {
    public GameObject GameOverObject;
    public Sprite[] BodySprites;
    public SpriteRenderer Body;
    private Rigidbody2D body;
    private Animator animator;
    private GameOverTitle title;
    private PlayerState state;
    private Vector3 speed = Vector3.right * 2f;
    private float dashVelocity;
    private float dashTime = 0f;
    private int batteryValue;
    public int Battery
    {
      get { return batteryValue; }
      set
      {
        batteryValue = Mathf.Clamp(value, 0, 5);
        if(Level.Instance != null)
          Level.Instance.ChargeGame.Value = batteryValue;

        Body.sprite = BodySprites[batteryValue];
      }
    }
    private int dashPower = 1;
    private bool go;

    public enum PlayerState { MOVING, CHARDGING, REACTOR, NAVIGATION, GAMEOVER };

    void Start()
    {
      body = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();
      state = PlayerState.MOVING;
      Battery = 5;
      go = false;
    }
    void Update()
    {
      if (go) return;
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
        case PlayerState.GAMEOVER:
          go = true;
          Fade.Instance.FadeOut(0.5f);
          Invoke("ShowGameOver", 0.5f);
          break;
      }
    }
    public void GameOver(GameOverTitle title)
    {
      this.title = title;
      state = PlayerState.GAMEOVER;
    }
    void ShowGameOver()
    {
      GameOverObject.SetActive(true);
      GameOverObject.GetComponent<GameOver>().SetGameOver(title);
      Level.Instance.gameObject.SetActive(false);
    }
    private void Termocontrolling()
    {
      if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        Level.Instance.NuclearGame.Up();
      if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        Level.Instance.NuclearGame.Down();
      if (Input.GetKeyDown(KeyCode.E))
      {
        Level.Instance.NuclearPanel.BigPanel.Hide();
        Move();
      }
    }

    private void Charging()
    {
      if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        Battery++;
      if (Input.GetKeyDown(KeyCode.E))
      {
        Level.Instance.ChargePanel.BigPanel.Hide();
        Move();
      }
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
      {
        Level.Instance.NavigationPanel.BigPanel.Hide();
        Move();
      }
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
      if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && dashTime < Mathf.Epsilon)
        velocity--;
      if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && dashTime < Mathf.Epsilon)
        velocity++;
      if (Input.GetKeyDown(KeyCode.Space) && velocity != 0 && dashTime < Mathf.Epsilon && Battery >= dashPower)
      {
        dashVelocity = velocity * 3f;
        dashTime = 0.2f;
        Battery -= dashPower;
      }

      if (Mathf.Abs(velocity) < Mathf.Epsilon && dashTime < Mathf.Epsilon)
      {
        animator.Play("player_idle");
        transform.localScale = new Vector3(2, 2, 2);
      }
      else
      {
        animator.Play("player_run");
        transform.localScale = velocity + dashVelocity > 0 ? new Vector3(2, 2, 2) : new Vector3(-2, 2, 2);
      }

      if (dashTime > 0)
      {
        dashTime -= Time.deltaTime;
        body.velocity = speed * dashVelocity;
      }
      else
      {
        dashVelocity = 0;
        body.velocity = speed * velocity;
      }

      if (Input.GetKeyDown(KeyCode.E))
        if (Interract())
        {
          body.velocity = Vector3.zero;
          dashTime = 0;
        }
    }

    private bool Interract()
    {
      var res = false;
      if (Level.Instance.NavigationPanel.Near)
      {
        state = PlayerState.NAVIGATION;
        Level.Instance.NavigationPanel.BigPanel.Show();
        animator.Play("player_work");
        res = true;
      }
      if (Level.Instance.ChargePanel.Near)
      {
        state = PlayerState.CHARDGING;
        Level.Instance.ChargePanel.BigPanel.Show();
        animator.Play("player_work");
        res = true;
      }
      if (Level.Instance.NuclearPanel.Near)
      {
        state = PlayerState.REACTOR;
        Level.Instance.NuclearPanel.BigPanel.Show();
        animator.Play("player_work");
        res = true;
      }
      return res;
    }
  }
}