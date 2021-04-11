using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public GameManager gameManager;
  public Transform healthBar;
  public Path route;
  private Waypoint[] myPathThroughLife;
  public int coinWorth;
  public float health;
  public float speed = .25f;
  public float maxHealth;
  public int attack;
  private int index = 0;
  private Vector3 nextWaypoint;
  private bool stop;

  void Awake()
  {
    stop = false;
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    myPathThroughLife = route.path;
    transform.position = myPathThroughLife[index].transform.position;
    health = maxHealth;
    Damage();
    Recalculate();
  }

  void Update()
  {
    if (!stop)
    {
      if ((transform.position - myPathThroughLife[index + 1].transform.position).magnitude < .1f)
      {
        index += 1;
        Recalculate();
      }

      Vector3 moveThisFrame = nextWaypoint * (Time.deltaTime * speed);
      transform.Translate(moveThisFrame);
    }
  }


  private void Recalculate()
  {
    if (myPathThroughLife[index].name != "End" )
    {
      nextWaypoint = (myPathThroughLife[index + 1].transform.position - myPathThroughLife[index].transform.position).normalized;
    }
    else
    { 
      gameManager.hurtMe(this);
      stop = true;
    }
  }

  public bool Strike()
  {
    health -= 10;
    Damage();
    return health <= 0;
  }

  public void Damage()
  {
    var localScale = healthBar.localScale;
    Vector3 newHealth = new Vector3((health / maxHealth), localScale.y, localScale.z);
    healthBar.localScale = newHealth;
  }

  public void OnDestroy()
  {
    gameManager.enemyCounter(-1);
  }
}
