using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public void ChangeScene(string scene) {
		SceneManager.LoadScene(scene);
	}

}
