using UnityEngine;
using TMPro;

// Controller for animating movements of cards
public class HandCard : MonoBehaviour {

	// The percentage of the animation that is completed
	public float gotoProgress = 0.0f;
	// The card this is representing
	public Card card {
		get { return _card; }
		set {
			_card = value;
			Transform title = transform.Find("Card").Find("Canvas").Find("Title");
			title.GetComponent<TextMeshProUGUI>().SetText(value.plant.title);
		}
	}
	private Card _card;
	// If this card was clicked last
	public bool selected {
		get { return _selected; }
		set {
			_selected = value;
			transform.Find("Card").Find("Outline").GetComponent<MeshRenderer>().enabled = value;
		}
	}
	private bool _selected = false;


	// If the card has a target set
	private bool hasTarget = false;
	// The transform at which the animation started
	private Vector3 startPosition = Vector3.zero;
	// The transform at which the animation should stop
	private Vector3 targetPosition = Vector3.zero;

	public void GotoHand(Vector3 handPosition) {
		SetTarget(handPosition);
		GetComponent<Animator>().SetTrigger("GotoHand");
	}

	private void SetTarget(Vector3 targetPosition) {
		startPosition = transform.position;
		this.targetPosition = targetPosition;
		hasTarget = true;
	}

	public void UnsetTarget() {
		transform.position = targetPosition;
		hasTarget = false;
	}

	void OnAnimatorMove() {
		if (hasTarget) {
			transform.position = Vector3.Lerp(startPosition, targetPosition, gotoProgress);
		}
	}

}
