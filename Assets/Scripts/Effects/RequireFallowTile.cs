using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireFallowTile : Effect
{

    public override void applyEffect(PlotContext ctx)
    {
        List<GameObject> plots = ctx.getNeighbors(x, y);
        bool hasFallow = false ;
        foreach (GameObject prop in plots)
        {
            Plot plot = prop.GetComponent<Plot>();
            GameObject p = plot.getPlant();
            if (p == null)
                continue;
            Plant plnt = p.GetComponent<PlantBehavior>().p;
            Debug.Log(plnt);
            if (plnt.title == "Fallow")
                hasFallow |= true;
        }
        Debug.Log(hasFallow);
        if (!hasFallow)
            ctx.getPlot(x, y).GetComponent<Plot>().shouldKill |= true;
    }
}
