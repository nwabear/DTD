using System;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    public GameObject player;
    public List<Vector3> path;

    public float moveSpeed;

    private float timer;

    private Vector3 curPos;

    private int curNode = 0;
    // Start is called before the first frame update
    void Start()
    {
        GoToNextNode();
    }

    void GoToNextNode()
    {
        timer = 0;
        curPos = path[curNode];
    }

    public void resetPos()
    {
        curNode = 0;
        curPos = path[0];
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * moveSpeed;
        if (player.transform.position != curPos)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, curPos, (float) 0.05);
        }
        else
        {
            curNode++;
            GoToNextNode();
        }
    }

    public Vector3 getNextNode()
    {
        return curPos;
    }

    public int getCurNode()
    {
        return curNode;
    }
}
