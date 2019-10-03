using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public TileCreator tc;
    public Targeting tower;
    public CashCounter cc;

    public Button upgrade1, upgrade2, upgrade3, sell;

    private List<int> costs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tower != null)
        {
            costs = tower.getCosts();
            upgrade1.GetComponentInChildren<Text>().text = "Fire Rate: $" + costs[0];
            upgrade2.GetComponentInChildren<Text>().text = "Bullet Speed: $" + costs[1];
            upgrade3.GetComponentInChildren<Text>().text = "Bullet Damage: $" + costs[2];
            sell.GetComponentInChildren<Text>().text = "Sell: +$" + costs[3];
        }
    }

    public void interact(Button which)
    {
        int upgrade = -1;
        switch (which.name)
        {
            case "u00":
                upgrade = 0;
                break;
            
            case "u01":
                upgrade = 1;
                break;
            
            case "u02":
                upgrade = 2;
                break;
            
            case "Sell":
                upgrade = 3;
                break;
        }

        if (upgrade != -1)
        {
            int cost = tower.getCosts()[upgrade];
            if (upgrade != 3)
            {
                if (cc.cash >= cost && tower.upgrade(upgrade))
                {
                    cc.cash -= cost;
                }
            }
            else
            {
                tc.sellTower(tower.gameObject);
                cc.cash += cost;
            }
        }
    }
}
