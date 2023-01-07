using UnityEngine;
using System.Collections.Generic;

public class DrawStack : MonoBehaviour {

	private List<Card> cards;

	void Start() {
		// TODO
		cards = new List<Card>();
		cards.Add(new Card());
		cards.Add(new Card());
	}

	public Card Pop() {
		if (cards.Count == 0) {
			return null;
		}

		Card card = cards[cards.Count - 1];
		cards.RemoveAt(cards.Count - 1);
		UpdateHeight();

		return card;
	}

	public Vector3 GetTopCardPosition() {
		// TODO
		return transform.position;
	}

	private void UpdateHeight() {
		// TODO
	}

}
