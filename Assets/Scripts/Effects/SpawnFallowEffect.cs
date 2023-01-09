using System.Collections;
using System.Collections.Generic;

public class SpawnFallowEffect : Effect
{

    public override void applyEffect(PlotContext ctx)
    {
        var plots = ctx.getNeighbors(x, y);
        foreach (var prop in plots)
        {
            Plot plot = prop.GetComponent<Plot>();
            if (plot.getPlant() == null)
                plot.spawnPlant = new Plot.ReplacePlant(plot.fallowPlant, false);
        }
    }
}
