using UnityEngine;

// Controller to manage the cards in hand
// 
// Cards are saved as children of the controllers gameObject
public class HandController : MonoBehaviour {

	public float maxHandWidth = 1f;
	public float maxCardDistance = 0.2f;
	public GameObject cardPrefab;

	void Update() {
		if (Input.GetButtonDown("Submit")) {
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
		UpdateCardPositions();

		return true;
	}

	public void UpdateCardPositions() {
		const float cardWidth = 0.2f;
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
			child.GetComponent<HandCard>().GotoHand(target);
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

	public void PlayCard(HandCard card) {
		card.transform.parent = null;
		Destroy(card.gameObject);
		UpdateCardPositions();
	}

}
