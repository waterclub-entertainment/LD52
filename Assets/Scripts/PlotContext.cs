using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotContext
{
    //ravelled, X-major
    private GameObject[] plots;
    private int X, Y;

    public PlotContext(int X, int Y, Func<GameObject> _constructor)
    {
        this.X = X;
        this.Y = Y;

        plots = new GameObject[X * Y];
        for (int x = 0; x < X; x++)
        {
            for (int y = 0; y < Y; y++)
            {
                plots[x * Y + y] = _constructor();
            }
        }
    }

    public GameObject getPlot(int x, int y)
    {
        if (!validate(x, y))
            throw new ArgumentOutOfRangeException();
        return plots[x * Y + y];
    }

    public List<GameObject> getNeighbors(int x, int y)
    {
        List<GameObject> plts = new List<GameObject>();

        if (validate(x - 1, y))
            plts.Add(getPlot(x - 1, y));
        if (validate(x + 1, y))
            plts.Add(getPlot(x + 1, y));
        if (validate(x, y - 1))
            plts.Add(getPlot(x, y - 1));
        if (validate(x, y + 1))
            plts.Add(getPlot(x, y + 1));
        
        return plts;
    }

    public void applyToAll(Action<Plot> op)
    {
        foreach (var plot in plots)
        {
            op(plot.GetComponent<Plot>());
        }
    }
    public void applyToAllEx(Action<Plot, PlotContext, int, int> op)
    {
        for (int x = 0; x < X; x++)
        {
            for (int y = 0; y < Y; y++)
                op(getPlot(x, y).GetComponent<Plot>(), this, x, y);
        }
    }

    private bool validate(int x, int y)
    {
        return (x >= 0 && x < X && y >= 0 && y < Y);

    }
}