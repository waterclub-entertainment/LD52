using UnityEngine;
using System.Collections.Generic;

// Abstract class for plants
public abstract class Plant : ScriptableObject {

    public GameObject prefab;
    public string title;

    protected List<Effect> effects = new List<Effect>();

    // The reward if the plant was harvested now or null if the plant cannot be
    // harvested at the moment
    public abstract Card HarvestReward();

	// Progress the growth of the plant by the given season
	//
	// Should return true if the plant stays alive and false if it dies
	public abstract bool Progress(Season season);

	// The growth stage of the plant as a number between 0 and 1
	public abstract float GrowthStage();

    public void getEffects(Season season, ref List<Effect> effects)
    {
        foreach (Effect e in this.effects)
        {
            if (e.shouldApply(season, GrowthStage()))
                effects.Add(e);
        }
    }
    public void RegisterEffect(Effect e)
    {
        effects.Add(e);
    }

    public abstract Season? NextSeasonEffect();
}
