using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFallowEffect : Effect
{

    public override void applyEffect(PlotContext ctx)
    {
        var plots = ctx.getNeighbors(x, y);
        foreach (var prop in plots)
        {
            Plot plot = prop.GetComponent<Plot>();
            GameObject p = plot.getPlant();
            if (p == null)
                continue;
            Plant plnt = p.GetComponent<PlantBehavior>().p;
            if (plnt.title == "Fallow")
            {
                plot.shouldKill |= true;
                plot.spawnsFallow &= false;
            }
        }
    }
}
