using UnityEngine;
using System;
using System.Collections.Generic;

// Plant that needs a specific sequence of seasons to grow
[CreateAssetMenu(menuName = "Plant/Sequence Plant")]
public class SequencePlant : Plant {

	// The seasons (in order) needed to grow the plant
	public Season[] neededSeasons; //force initialize over proxy?
                                   // The seasons that kill the plant
    public Season[] forbiddenSeasons; //force initialize over proxy?

	// The current stage of the plant
	private int stage = 0;

    public int getStage()
    {
        return stage;
    }

	public override Card HarvestReward() {
		if (stage == neededSeasons.Length) {
			SequencePlant p = Instantiate(this);
			p.stage = 0;
			return new Card(p);
		}

		return null; //is this an unreachable state? -- No, early harvest
	}

	public override bool Progress(Season season) {
        if (stage == neededSeasons.Length)
            return false; //plants that are too ripe die. Maybe change or replace later.
		if (season == neededSeasons[stage]) {
			stage++;
		}

		// Plan survives if the season is not in forbiddenSeasons
		return (forbiddenSeasons == null || forbiddenSeasons.Length == 0) ? true : Array.IndexOf(forbiddenSeasons, season) == -1;
	}

    public override void getEffects(Season season, ref List<Effect> effects, int x, int y)
    {
        //no effect
    }

    public override float GrowthStage()
    {
        return ((float) stage) / ((float) neededSeasons.Length);
    }
}
