using UnityEngine;

// Controller to manage the cards in hand
// 
// Cards are saved as children of the controllers gameObject
public class HandController : MonoBehaviour, SeasonHandler.SeasonChangeListener {

	public float maxHandWidth = 1f;
	public float maxCardDistance = 0.2f;
	public GameObject cardPrefab;
	public int drawPerTurn = 3;
	public SeasonHandler seasonHandler;
	public Card[] initialHand;

	void Start() {
		foreach (Card card in initialHand) {
			GameObject newCard = GameObject.Instantiate(cardPrefab, transform);
			newCard.GetComponent<HandCard>().card = card;
		}
		UpdateCardPositions(false);
		seasonHandler.listeners.Add(this);
	}

	void Update() {
		if (Input.GetButtonDown("Submit")) { // TODO: Remove
			Draw();
		}
	    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		HandCard hitCard = null;
		if (Physics.Raycast(ray, out hit)) {
			hitCard = hit.transform.GetComponentInParent<HandCard>();
        }
		foreach (Transform child in transform) {
			HandCard childCard = child.GetComponent<HandCard>();
			if (Input.GetMouseButtonDown(0) && hitCard != null) {
				childCard.selected = childCard == hitCard;
			}
			child.GetComponent<Animator>().SetBool("Hover", childCard == hitCard);
		}
	}

	public bool Draw() {
		DrawStack stack = GameObject.FindObjectOfType<DrawStack>();
		Card card = stack.Pop();
		if (card == null) {
			return false;
		}

		GameObject newCard = GameObject.Instantiate(cardPrefab, transform);
		newCard.transform.position = stack.GetTopCardPosition();
		newCard.GetComponent<HandCard>().card = card;
		UpdateCardPositions(true);

		return true;
	}

	private void UpdateCardPositions(bool animate) {
		const float cardWidth = 0.8f;
		float cardDistance = Mathf.Min(
			maxCardDistance,
			(maxHandWidth - cardWidth) / (transform.childCount - 1));
		float handWidth = cardDistance * (transform.childCount - 1) + cardWidth;
		float firstCardX = -handWidth / 2.0f + cardWidth / 2;
		for (int i = 0; i < transform.childCount; i++) {
			Transform child = transform.GetChild(i);
			Vector3 target =  new Vector3(
				transform.position.x + firstCardX + i * cardDistance,
				transform.position.y,
				transform.position.z
			);
			if (animate) {
				child.GetComponent<HandCard>().GotoHand(target);
			} else {
				child.transform.position = target;
				child.GetComponent<Animator>().Play("Hand");
			}
		}
	}

	public HandCard GetSelected() {
		foreach (HandCard handCard in GetComponentsInChildren<HandCard>()) {
			if (handCard.selected) {
				return handCard;
			}
		}
		return null;
	}

	public void PlayCard(HandCard card, Vector3 worldPosition) {
		card.transform.parent = null;
		card.Play(worldPosition);
		UpdateCardPositions(true);
	}

    public void onSeasonChange(Season s)
    {
		for (int i = 0; i < drawPerTurn; i++) {
			Draw();
		}
    }
}
