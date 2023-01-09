using UnityEngine;

public class HarvestEffectBehaviour : MonoBehaviour {

	public float duration = 1.0f;

	private Vector3 startPosition;
	private float time = 0.0f;

	void Start() {
		startPosition = transform.position;
	}

	void Update() {
		time += Time.deltaTime;
		if (time >= duration) {
			Destroy(gameObject);
			return;
		}

		HarvestStack harvestStack = FindObjectOfType<HarvestStack>();

		float progress = time / duration;
		transform.position =
			Vector3.Lerp(startPosition, harvestStack.transform.position, progress)
			+ new Vector3(0, 1.0f - progress * progress, 0);
	}

}
