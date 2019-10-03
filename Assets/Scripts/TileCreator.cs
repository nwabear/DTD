using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileCreator : MonoBehaviour
{
   public GameObject corner, tilePrefab, obsticle, turret, path, path2, start, end, range, sm;
   public UpgradeMenu um;
   public CashCounter cc;
   public Camera camera;

   public int size;

   public int selection = -1;

   private GameObject recent;
   // Start is called before the first frame update
   void Start()
   {
       Vector3 pos = corner.transform.position;
       for (double x = pos.x; x < size + pos.x; x++)
       {
           for (double y = pos.y; y < size + pos.y; y++)
           {
               GameObject tile = Instantiate(tilePrefab, new Vector3((float)(x), (float)(size - y - 1)), transform.rotation);
               tile.transform.parent = transform;
           }
       }
   }

   // Update is called once per frame
   void Update()
   {
       if (Input.GetMouseButtonDown(0))
       {
           Ray ray = camera.ScreenPointToRay(Input.mousePosition);
           RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer("Tile"));
          
           if (hit.collider != null && hit.collider.gameObject.transform.position != start.transform.position && hit.collider.gameObject.transform.position != end.transform.position)
           {
               bool found = false;
               Vector3 pos = hit.collider.gameObject.transform.position;
               foreach (Transform obs in obsticle.GetComponentsInChildren<Transform>())
               {
                   Vector3 obsPos = obs.position;
                   if (pos.x == obsPos.x && pos.y == obsPos.y)
                   {
                       sm.SetActive(false);
                       um.gameObject.SetActive(true);
                       um.tower = obs.gameObject.GetComponent<Targeting>();
                       range.transform.position = um.tower.transform.position;
                       float size = um.tower.gameObject.GetComponent<CircleCollider2D>().radius;
                       range.transform.localScale = new Vector2(size, size);
                       found = true;
                   }
               }

               if (!found)
               {
                   if (selection != -1)
                   {
                       if (cc.cash >= turret.GetComponent<Targeting>().startCost)
                       {
                           GameObject tur = Instantiate(turret, pos, transform.rotation);
                           tur.transform.parent = obsticle.transform;

                           cc.cash -= turret.GetComponent<Targeting>().startCost;
                           recent = tur;
                           recalc();
                       }

                       selection = -1;
                   }
                   else
                   {
                       deselect();
                   }
               }

               foreach (Transform t in path.GetComponentsInChildren<Transform>())
               {
                   if (t.gameObject != path)
                   {
                       Destroy(t.gameObject);
                   }
               }
           }
       }
   }

   public void sellTower(GameObject tower)
   {
       tower.transform.parent = null;
       Destroy(tower);
       deselect();
       recalc();
   }

   public void select(int selection)
   {
       this.selection = selection;
   }

   public void deselect()
   {
       range.transform.localScale = new Vector2(0, 0);
       um.gameObject.SetActive(false);
       sm.SetActive(true);
   }

   public void recalc()
   {
       if (!path2.GetComponent<EnemyPathCalc>().CalcPath())
       {
           Destroy(recent);
       }
       else
       {
           path.GetComponent<Path>().CalcPath();
       }
   }
}
