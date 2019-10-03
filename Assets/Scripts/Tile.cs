using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    private List<Tile> tiles = new List<Tile>();
    private Tile prev;
    private Vector3 pos;

    private bool visited = false;
    public bool isOccupied = false;

    private int distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPos(Vector3 pos)
    {
        this.pos = pos;
    }

    public Vector3 getPos()
    {
        return pos;
    }

    public void addEdge(Tile tile)
    {
        tiles.Add(tile);
    }
    
    public void prepForSearch(bool isStart) {
        distance = Int32.MaxValue;
        if(isStart) {
            distance = 0;
        }
        visited = false;
    }

    public List<Tile> getEdges()
    {
        return tiles;
    }
    
    public void setDistance(int distance) {
        this.distance = distance;
    }

    public bool wasVisited() {
        return visited;
    }

    public void setPrev(Tile prev) {
        this.prev = prev;
    }

    public Tile getPrev()
    {
        return prev;
    }

    public int getDistance() {
        return distance;
    }
}
