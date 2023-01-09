using UnityEngine;

public class TutorialBehaviour : MonoBehaviour, SeasonHandler.SeasonChangeListener {

	public SeasonHandler seasonHandler;

	private int stage = 0;

	void Start() {
		seasonHandler.listeners.Add(this);
	}

	public void onSeasonChange(Season s) {
		stage++;
		if (stage == 1) {
			transform.GetChild(0).gameObject.SetActive(false);
			transform.GetChild(1).gameObject.SetActive(true);
		} else if (stage == 2) {
			transform.GetChild(1).gameObject.SetActive(false);
			transform.GetChild(2).gameObject.SetActive(true);
		} else if (stage == 3) {
			transform.GetChild(2).gameObject.SetActive(false);
		}
	}

}
