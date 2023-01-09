using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonNeighborhood : Effect
{
    public Season s;
    public bool disableSeason;

    public override void applyEffect(PlotContext ctx)
    {
        var plots = ctx.getNeighbors(x, y);
        foreach (var prop in plots)
        {
            Plot plot = prop.GetComponent<Plot>();
            if (disableSeason)
                plot.season = (Season)((int)plot.season & ~(int)s);
            else
                plot.season = (Season)((int)plot.season | (int)s);
            Debug.Log(plot.season);
        }
    }
}
