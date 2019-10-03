using System;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    private List<GameObject> objs = new List<GameObject>();
    private int timer = 0;
    public int shotDelay, bulletSpeed, bulletDamage;
    public int startCost = 100;

    public GameObject turret, bullet;

    private int u0 = 0, u1 = 0, u2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        var circleCollider = gameObject.GetComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (objs.Count > 0)
        {
            if (timer % shotDelay == 0)
            {
                Shoot();
            }

            GameObject close = objs[0];
            for (int i = 0; i < objs.Count; i++)
            {
                if (objs[i] != null)
                {
                    if (objs[i].GetComponent<Navigator>().getCurNode() > close.GetComponent<Navigator>().getCurNode())
                    {
                        close = objs[i];
                    }
                }
                else
                {
                    objs.Remove(objs[i]);
                }
            }
            Vector3 targ = close.transform.position;
            targ.z = 0f;

            Vector3 objectPos = turret.transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            turret.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            timer++;
        }
        else
        {
            timer = shotDelay - 1;
        }
    }

    void Shoot()
    {
        GameObject clone = Instantiate(bullet, GetComponent<Rigidbody2D>().transform.position, turret.transform.rotation);
        clone.GetComponent<Bullet>().damage = bulletDamage;
        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.up * bulletSpeed);
    }

    public bool upgrade(int type)
    {
        switch (type)
        {
            case 0:
                if (u0 < 4)
                {
                    shotDelay -= 5;
                    u0++;
                    return true;
                }
                break;
            
            case 1:
                if (u1 < 4)
                {
                    bulletSpeed += 100;
                    u1++;
                    return true;
                }
                break;
            
            case 2:
                if (u2 < 4)
                {
                    bulletDamage += 1;
                    u2++;
                    return true;
                }
                break;
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            objs.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            objs.Remove(other.gameObject);
        }
    }

    public List<int> getCosts()
    {
        List<int> costs = new List<int>();
        costs.Add((u0 + 1) * 100);
        costs.Add((u1 + 1) * 50);
        costs.Add( (u2 + 1) * 150);
        costs.Add((u0 * 20) + (u1 * 10) + (u2 * 30) + 40);

        return costs;
    }
}
