using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyPathCalc : MonoBehaviour
{
    public GameObject curPos, startPos, obsticles, enemies;
    
    public int size;

    private List<Vector3> defPath;
    // Start is called before the first frame update
    void Start()
    {
        CalcPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool CalcPath()
    {
        List<Tile> Q = new List<Tile>();
        List<Navigator> enems = new List<Navigator>(enemies.GetComponentsInChildren<Navigator>());
        Vector3 pos = curPos.transform.position;
        int curTile = 0;
        
        List<Vector3> ePosList = new List<Vector3>();

        try
        {
            foreach (Navigator n in enems)
            {
                ePosList.Add(n.getNextNode());
            }
        }
        catch (Exception e)
        {
            Debug.Log("bad");
        }

        for (double x = pos.x; x < pos.x + size; x++)
        {
            for (double y = pos.y; y < pos.y + size; y++)
            {
                Q.Add(new Tile());
                Q[curTile].prepForSearch(false);
                Q[curTile].setPos(new Vector3((float) x, (float) (size - y - 1)));
                if (Q[curTile].getPos().Equals(transform.position))
                {
                    Q[curTile].setDistance(0);
                }

                bool isTaken = false;

                foreach (Transform t in obsticles.GetComponentsInChildren<Transform>())
                {
                    if (t.position == Q[curTile].getPos())
                    {
                        isTaken = true;
                    }
                }

                Q[curTile].isOccupied = isTaken;

                curTile++;
            }
        }

        curTile = 0;
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (x > 0 && !Q[curTile - 10].isOccupied)
                {
                    Q[curTile].addEdge(Q[curTile - 10]);
                }

                if (x < 9 && !Q[curTile + 10].isOccupied)
                {
                    Q[curTile].addEdge(Q[curTile + 10]);
                }

                if (y > 0 && !Q[curTile - 1].isOccupied)
                {
                    Q[curTile].addEdge(Q[curTile - 1]);
                }

                if (y < 9 && !Q[curTile + 1].isOccupied)
                {
                    Q[curTile].addEdge(Q[curTile + 1]);
                }

                curTile++;
            }
        }

        bool isDone = false;
        Tile u = Q[0];

        while (Q.Count != 0 && !isDone)
        {
            u = Q[0];
            foreach (Tile t in Q)
            {
                if (t.getDistance() < u.getDistance())
                {
                    u = t;
                }
            }

            if (u.getDistance() > Int16.MaxValue)
            {
                isDone = true;
            }
            else
            {
                Q.Remove(u);
                if (ePosList.Contains(u.getPos()))
                {
                    List<Vector3> temp = new List<Vector3>();
                    Tile curNode = u;
                    temp.Add(u.getPos());
                    while (curNode.getPrev() != null)
                    {
                        curNode = curNode.getPrev();
                        temp.Add(curNode.getPos());
                    }

                    temp.Add(transform.position);
                    Navigator n = enems[ePosList.IndexOf(u.getPos())];
                    n.gameObject.GetComponent<Navigator>().path = temp;
                    n.resetPos();
                }

                if (startPos.transform.position == u.getPos())
                {
                    List<Vector3> temp = new List<Vector3>();
                    Tile curNode = u;
                    while (curNode.getPrev() != null)
                    {
                        temp.Add(curNode.getPos());
                        curNode = curNode.getPrev();
                    }

                    temp.Add(transform.position);
                    defPath = temp;
                }

                foreach (Tile t in u.getEdges())
                {
                    int alt = u.getDistance() + 1;
                    if (alt < t.getDistance())
                    {
                        t.setDistance(alt);
                        t.setPrev(u);
                    }
                }
            }
        }

        ePosList.Add(startPos.transform.position);

        foreach (Tile t in Q)
        {
            if (ePosList.IndexOf(t.getPos()) != -1)
            {
                return false;
            }
        }

        return true;
    }

    public List<Vector3> getDefPath()
    {
        return defPath;
    }
}
