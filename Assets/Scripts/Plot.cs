using System;
using System.Collections;
using System.Collections.Generic;

public class Plot
{
    Nullable<Season> actualSeason; //this keeps track of the base state. Relevant to apply multiple mutations on tick

    Nullable<Season> season;

    Season guarded;

    int tickSize;

    Plant plant;

    public void reset()
    {
        actualSeason = null;
        season = null;
        tickSize = 1;
        effects.RemoveAll();
    }

    public void setSeasonState(Season season, bool isDefault = false)
    {
        this.season = season;
        
        if (isDefault)
            actualSeason = season;
    }

    public void computeEffects(Season season, ref List<Effect> effects)
    {
        plant.getEffects(season, ref effects);
    }

    public void apply()
    {
        if (plant != null)
            plant.Progress(season.Value);
        else
        {
            //handle dissemination and spawning of new plants as mutations of neighbors?
        }
    }
}
