using UnityEngine;
using System.Collections.Generic;

public class DrawStack : MonoBehaviour {

	public float cardThickness = 0.005f;
	public Card[] initialStack;

	private List<Card> cards;

	void Start() {
		// TODO
		cards = new List<Card>();
		foreach (Card card in initialStack) {
			cards.Add(Instantiate(card));
		}
		UpdateHeight();
	}

	public Card Pop() {
		if (cards.Count == 0) {
			HarvestStack harvestStack = GameObject.FindObjectOfType<HarvestStack>();
			Card[] rewards = harvestStack.Empty();
			// Shuffle
			for (int i = 0; i < rewards.Length; i++) {
				int r = Random.Range(i, rewards.Length);
				cards.Add(rewards[r]);
				rewards[r] = rewards[i];
			}
		}
		if (cards.Count == 0) {
			Debug.Log("Game over"); // TODO
			return null;
		}

		Card card = cards[cards.Count - 1];
		cards.RemoveAt(cards.Count - 1);

		UpdateHeight();

		return card;
	}

	public Vector3 GetTopCardPosition() {
		return transform.position + new Vector3(0, (cards.Count + 1) * cardThickness, 0);
	}

	private void UpdateHeight() {
		if (cards.Count == 0) {
			GetComponentInChildren<MeshRenderer>().enabled = false;
		} else {
			GetComponentInChildren<MeshRenderer>().enabled = true;
			transform.localScale = new Vector3(1, cards.Count * cardThickness, 1);
		}
	}

}
