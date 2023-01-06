using UnityEngine;
using System;

// Plant that needs a specific sequence of seasons to grow
[CreateAssetMenu(menuName = "Plant/Sequence Plant")]
public class SequencePlant : Plant {

	// The seasons (in order) needed to grow the plant
	public Season[] neededSeasons;
	// The seasons that kill the plant
	public Season[] forbiddenSeasons;
	// The reward for the plant
	public Card harvestReward;

	// The current stage of the plant
	private int stage = 0;

	public override Card HarvestReward() {
		if (stage == neededSeasons.Length) {
			return harvestReward;
		}

		return null;
	}

	public override bool Progress(Season season) {
		if (season == neededSeasons[stage]) {
			stage++;
		}

		// Plan survives if the season is not in forbiddenSeasons
		return Array.IndexOf(forbiddenSeasons, season) == -1;
	}

}
