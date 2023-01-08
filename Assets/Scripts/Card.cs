using UnityEngine;

[CreateAssetMenu(menuName = "Plant/Card")]
public class Card : ScriptableObject {

	// The plant that can be sown with this card
	public Plant plant;
	public GameObject prefab;

	public Card(Plant plant) {
		this.plant = plant;
	}

}
