using UnityEngine;
using System.Collections.Generic;

public class HarvestStack : MonoBehaviour {

	public float cardThickness = 0.005f;

	private List<Card> rewards;

	void Start() {
		rewards = new List<Card>();
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
			GetComponentInChildren<MeshRenderer>().enabled = false;
		} else {
			GetComponentInChildren<MeshRenderer>().enabled = true;
			transform.localScale = new Vector3(1, rewards.Count * cardThickness, 1);
		}
	}

}
