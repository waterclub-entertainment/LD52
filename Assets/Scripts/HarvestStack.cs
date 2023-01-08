using UnityEngine;
using System.Collections.Generic;

public class HarvestStack : MonoBehaviour {

	public float cardThickness = 0.005f;

	private List<Card> rewards;

	void Start() {
		rewards = new List<Card>();
		UpdateHeight();
	}

	public void Add(Card card) {
		rewards.Add(card);
		UpdateHeight();
	}

	public Card[] Empty() {
		Card[] array = rewards.ToArray();
		rewards.Clear();
		UpdateHeight();
		return array;
	}

	private void UpdateHeight() {
		if (rewards.Count == 0) {
			foreach (Transform child in transform) {
				child.gameObject.SetActive(false);
			}
		} else {
			foreach (Transform child in transform) {
				child.gameObject.SetActive(true);
			}
			transform.localScale = new Vector3(1, rewards.Count * cardThickness, 1);
		}
	}

}
