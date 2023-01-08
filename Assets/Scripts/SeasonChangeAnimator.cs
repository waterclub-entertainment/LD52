using UnityEngine;

public class SeasonChangeAnimator : MonoBehaviour, SeasonHandler.SeasonChangeListener {

	public SeasonHandler seasonHandler;

	void Start() {
		seasonHandler.listeners.Add(this);
	}

	public void onSeasonChange(Season s) {
		Debug.Log(s);
		Animator seasonChangeAnimator = GetComponent<Animator>();
        switch(s) {
            case Season.Winter:
                seasonChangeAnimator.SetTrigger("Winter");
                break;
            case Season.Spring:
                seasonChangeAnimator.SetTrigger("Spring");
                break;
            case Season.Summer:
                seasonChangeAnimator.SetTrigger("Summer");
                break;
            case Season.Autumn:
                seasonChangeAnimator.SetTrigger("Autumn");
                break;
        }
	}
	
}
