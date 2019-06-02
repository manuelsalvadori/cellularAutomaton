using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Cell
{
    private bool isAlive = false;
    private bool newState = false;
    public int2 coords;
    private GameObject quad;

    public bool IsAlive { get => isAlive; }

    public Cell(int2 coords, GameObject quad)
    {
        this.coords = coords;
        this.quad = quad;
        quad.GetComponent<MeshRenderer>().enabled = false;
    }

    internal void SetAlive(bool alive)
    {
        newState = alive;
        quad.GetComponent<MeshRenderer>().enabled = alive;
    }

    public void Update()
    {
        isAlive = newState;
    }

    public void Next(List<int> bRule, List<int> sRule)
    {
        var neighboors = GridGenerator.Instance.GetNeighboors(coords);
        var aliveCount = AliveNeighboorsCount(neighboors);

        if (isAlive)
            Survive(sRule, neighboors, aliveCount);
        else
            Birth(bRule, neighboors, aliveCount);
    }

    private void Birth(List<int> bRule, List<Cell> neighboors, int aliveCount)
    {
        foreach (var rule in bRule)
        {
            if (aliveCount == rule)
            {
                SetAlive(true);
                return;
            }
        }
    }

    private void Survive(List<int> sRule, List<Cell> neighboors, int aliveCount)
    {
        foreach (var rule in sRule)
        {
            if(aliveCount == rule)
                return;
        }
        SetAlive(false);
    }

    private int AliveNeighboorsCount(List<Cell> neighboors)
    {
        int count = 0;
        foreach (var neighboor in neighboors)
        {
            if (neighboor.isAlive)
                count ++;
        }
        return count;
    }
}
