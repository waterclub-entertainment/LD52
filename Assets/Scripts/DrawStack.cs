using UnityEngine;
using System.Collections.Generic;

public class DrawStack : MonoBehaviour {

	public float cardThickness = 0.005f;
	public Plant[] initialStack;

	private List<Card> cards;

	void Start() {
		// TODO
		cards = new List<Card>();
		foreach (Plant plant in initialStack) {
			cards.Add(new Card(plant));
		}
		UpdateHeight();
	}

	public Card Pop() {
		if (cards.Count == 0) {
			return null;
		}

		Card card = cards[cards.Count - 1];
		cards.RemoveAt(cards.Count - 1);

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
