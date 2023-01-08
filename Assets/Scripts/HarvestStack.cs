using UnityEngine;
using System.Collections.Generic;

public class HarvestStack : MonoBehaviour {

	private List<Card> rewards;

	void Start() {
		rewards = new List<Card>();
	}

	public void Add(Card card) {
		rewards.Add(card);
	}

	public Card[] Empty() {
		Card[] array = rewards.ToArray();
		rewards.Clear();
		return array;
	}

}
