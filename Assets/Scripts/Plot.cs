using System;
using System.Collections;
using System.Collections.Generic;

public class Plot
{
    Nullable<Season> actualSeason; //this keeps track of the base state. Relevant to apply multiple mutations on tick
    int x, y;

    Nullable<Season> season;

    bool guarded;
    int tickSize;

    Plant plant;

    public Plot(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void reset()
    {
        actualSeason = null;
        season = null;
        tickSize = 1;
        guarded = false;
        effects.RemoveAll();
    }

    //update pipeline calls
    public void setSeasonState(Season season, bool isDefault = false)
    {
        this.season = season;
        
        if (isDefault)
            actualSeason = season;
    }
    public void computeEffects(ref List<Effect> effects)
    {
        plant.getEffects(actualSeason, ref effects);
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


    public void setPlant(Plant p)
    {
        plant = p;
        plant.OnPlant(x, y);
    }

    //TODO add mutation calls for effects.
    bool setGuarded(bool guarding)
    {
        guarded = guarding;
    }


}
