using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameManager gameManager;

    public GameObject Tower;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("TowerBase"))
            {
                PlaceTower(hit.transform.position);
            }
        }
    }

    private void PlaceTower(Vector3 pos)
    {
        if (gameManager.BuyTower(100))
        {
            Instantiate(Tower, pos, Quaternion.identity);
        }
    }
}
