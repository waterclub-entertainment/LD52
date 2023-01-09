using UnityEngine;

public class TutorialBehaviour : MonoBehaviour, SeasonHandler.SeasonChangeListener {

	public SeasonHandler seasonHandler;

	void Start() {
		seasonHandler.listeners.Add(this);
	}

	public void onSeasonChange(Season s) {
		gameObject.SetActive(false);
	}

}
