using UnityEngine;

// Behaviour of a card in hand
public class HandCard : MonoBehaviour {

	private Card card = null;

	public void SetCard(Card card) {
		this.card = card;
		// TODO: Update texture accordingly
	}

}
