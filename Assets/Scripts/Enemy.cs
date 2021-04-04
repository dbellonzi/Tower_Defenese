using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public Transform healthBar;
  public Path route;
  private Waypoint[] myPathThroughLife;
  public int coinWorth;
  public float health;
  public float speed = .25f;
  public float maxHealth;
  private int index = 0;
  private Vector3 nextWaypoint;
  private bool stop = false;

  void Awake()
  {
    myPathThroughLife = route.path;
    transform.position = myPathThroughLife[index].transform.position;
    health = maxHealth;
    Damage();
    Recalculate();
  }

  void Update()
  {
    if (stop) return;
    if ((transform.position - myPathThroughLife[index + 1].transform.position).magnitude < .1f)
    {
      index += 1;
      Recalculate();
    }
    Vector3 moveThisFrame = nextWaypoint * (Time.deltaTime * speed);
    transform.Translate(moveThisFrame);
  }

  private void Recalculate()
  {
    if (index < myPathThroughLife.Length -1)
    {
      nextWaypoint = (myPathThroughLife[index + 1].transform.position - myPathThroughLife[index].transform.position).normalized;
    }
    else
    {
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
}
