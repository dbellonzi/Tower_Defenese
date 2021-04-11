using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameManager gameManager;
    public List<Enemy> toKill;
    public String name;
    public float strikeTime;
    public float time; 
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time % strikeTime != time && toKill.Count != 0)
        {
            if (toKill[0] == null)
            {
                toKill.RemoveAt(0);
            }
            else if (toKill[0].Strike())
            {
                Enemy rip = toKill[0];
                toKill.RemoveAt(0);
                gameManager.money += rip.coinWorth;
                Destroy(rip.gameObject);
            }    
            time = time % strikeTime;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Enemy>() != null)
        {
            toKill.Add(collider.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Enemy>() != null)
        {
            toKill.Remove(collider.GetComponent<Enemy>());
        }
    }
}
