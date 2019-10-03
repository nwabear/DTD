using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Path : MonoBehaviour
{
    public GameObject node, tileMap, curPos, startPos, endPos, obsticles;
    public int size;

    private Tilemap tm;
    // Start is called before the first frame update
    void Start()
    {
        tm = tileMap.GetComponent<Tilemap>();
        CalcPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalcPath()
    {
        tm = tileMap.GetComponent<Tilemap>();
        List<Tile> Q = new List<Tile>();
        Vector3 pos = curPos.transform.position;
        int curTile = 0;

        for (double x = pos.x; x < pos.x + size; x++)
        {
            for (double y = pos.y; y < pos.y + size; y++)
            {
                Q.Add(new Tile());
                Q[curTile].prepForSearch(false);
                Q[curTile].setPos(new Vector3((float)x, (float)(size - y - 1)));
                if (Q[curTile].getPos().Equals(startPos.transform.position))
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
            foreach(Tile t in Q){
                if (t.getDistance() < u.getDistance())
                {
                    u = t;
                }
            }

            Q.Remove(u);

            if (u.getPos().Equals(endPos.transform.position))
            {
                isDone = true;
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
        
        List<Vector3> path = new List<Vector3>();
        while (u.getPrev() != null)
        {
            path.Insert(0, u.getPos());
            u = u.getPrev();
        }

        GameObject s = Instantiate(node, startPos.transform.position, transform.rotation);
        s.transform.parent = transform;

        LineRenderer lineRend = GetComponent<LineRenderer>();
        lineRend.gameObject.SetActive(true);
        lineRend.SetWidth(0.05f, 0.05f);
        lineRend.SetVertexCount(path.Count + 1);
        
        lineRend.SetPosition(0, transform.position);

        foreach (Vector3 v in path)
        {
            GameObject t = Instantiate(node, v, transform.rotation);
            t.transform.parent = transform;
            lineRend.SetPosition(path.IndexOf(v) + 1, v);
        }
    }
}
