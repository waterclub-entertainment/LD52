using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldController : MonoBehaviour
{

    PlotContext ctx;


    // Start is called before the first frame update
    void Start()
    {
        ctx = new PlotContext();
    }

    void Step(Season season)
    {
        //reset plots for steps
        ctx.applyToAll((Plot plt) => { plt.reset(); plt.setSeasonState(season, true); });
        

        //iterate plant effects over season
        ctx.applyToAll((Plot plt, PlotContext ctx, int x, int y) => { plt.computeEffects(season, ctx, x, y); });
        //possibly collect effects first then sort and compute.....


        //update plants
        ctx.applyToAll((Plot plt) => { plt.apply();});

    }

    // Update is called once per frame
    void Update()
    {
        //handle animations i guess. maybe poll UI event?
    }
}
