using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlotContext
{
    //should be changable?
    const short Y = 4;
    const short X = 5;
    //ravelled, X-major
    private Plot[] plots;

    public PlotContext()
    {
        plots = new Plot[X * Y];
        for (int x = 0; x < X; x++)
        {
            for (int y = 0; y < Y; y++)
            {
                plots[x * Y + y] = new Plot(x, y);
                plots[x * Y + y].reset();
            }
        }
    }

    public Plot getPlot(int x, int y)
    {
        if (!validate(x, y))
            throw new ArgumentOutOfRangeException();
        return plots[x * Y + y];
    }

    public List<Plot> getNeighbors(int x, int y)
    {
        List<Plot> plots = new List<Plot>();

        if (validate(x - 1, y))
            plots.Add(getPlot(x - 1, y));
        if (validate(x + 1, y))
            plots.Add(getPlot(x + 1, y));
        if (validate(x, y - 1))
            plots.Add(getPlot(x, y - 1));
        if (validate(x, y + 1))
            plots.Add(getPlot(x, y + 1));

        return plots;
    }

    public void applyToAll(Action<Plot> op)
    {
        foreach (var plot in plots)
        {
            op(plot);
        }
    }
    public void applyToAll(Action<Plot, PlotContext> op)
    {
        foreach (var plot in plots)
        {
            op(plot, this);
        }
    }
    public void applytoAll(Action<Plot, PlotContext, int, int> op)
    {
        for (int x = 0; x < X; x++)
        {
            for (int y = 0; y < Y; y++)
                op(getPlot(x, y), this, x, y);
        }
    }

    private bool validate(int x, int y)
    {
        return (x >= 0 || x < X || y >= 0 || y < Y);

    }
}