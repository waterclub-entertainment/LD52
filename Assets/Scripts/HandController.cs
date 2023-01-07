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
		if (Input.GetMouseButtonDown(0)) {
		    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				HandCard hitCard = hit.transform.GetComponent<HandCard>();
				if (hitCard != null) {
					foreach (Transform child in transform) {
						HandCard childCard = child.GetComponent<HandCard>();
						childCard.selected = childCard == hitCard;
					}
				}
	        }
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
				transform.position.x + firstCardX + i * cardWidth,
				transform.position.y,
				transform.position.z
			);
			child.GetComponent<HandCard>().GotoHand(target);
		}
	}

}
