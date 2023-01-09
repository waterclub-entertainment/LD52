using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DrawStack : MonoBehaviour {

	public float cardThickness = 0.005f;
	public Card[] initialStack;

	private List<Card> cards;

	void Start() {
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
			SceneManager.LoadScene("Scenes/GameOver");
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
			foreach (Transform child in transform) {
				child.gameObject.SetActive(false);
			}
		} else {
			foreach (Transform child in transform) {
				child.gameObject.SetActive(true);
			}
			transform.localScale = new Vector3(1, cards.Count * cardThickness, 1);
		}
	}

}
