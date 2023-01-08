using UnityEngine;
using System.Collections.Generic;

// Abstract class for plants
public abstract class Plant : ScriptableObject {

	// The name of the plant
	public string title;
	public Sprite image;
    public GameObject prefab;

    // The reward if the plant was harvested now or null if the plant cannot be
    // harvested at the moment
    public abstract Card HarvestReward();

	// Progress the growth of the plant by the given season
	//
	// Should return true if the plant stays alive and false if it dies
	public abstract bool Progress(Season season);

	// The growth stage of the plant as a number between 0 and 1
	public abstract float GrowthStage();

    // TODO: Add method to receive sprite for plant
    public abstract void getEffects(Season season, ref List<Effect> effects, int x, int y);
}
