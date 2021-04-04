using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text PurseField;
    [SerializeField] private TMP_Text LifeField;
    [SerializeField] private Camera cam;

    private string moneyBase = "MONEY\n";
    private string lifeBase = "HEALTH\n";
    public int money;
    public int life;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateOSD();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.GetComponent<Enemy>())
            {
                Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                if (enemy.Strike())
                {
                    money += enemy.coinWorth;
                    Destroy(hit.collider.gameObject);
                    UpdateOSD();
                }
            }
        }
    }

    private void UpdateOSD()
    {
        PurseField.text = moneyBase + money;
        LifeField.text = lifeBase + life;
    }

    public bool BuyTower(int cost)
    {
        if (money < cost) return false;
        money -= cost;
        UpdateOSD();
        return true;
    }
}
