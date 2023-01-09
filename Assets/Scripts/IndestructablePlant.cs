using UnityEngine;

// Plant that needs a specific sequence of seasons to grow
[CreateAssetMenu(menuName = "Plant/Indestructable Plant")]
public class IndestructablePlant : Plant {

	public override Card HarvestReward() {
		return null;
	}

	public override bool Progress(Season season, Season actualSeason) {
		return true;
	}

    public override float GrowthStage()
    {
		return 1.0f;
    }

	public override Season? NextSeasonEffect() {
		return null;
	}
}

