using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public string preloadScene;

	private AsyncOperation loadOperation = null;

	void Start() {
		if (preloadScene.Length > 0) {
			loadOperation = SceneManager.LoadSceneAsync(preloadScene);
			loadOperation.allowSceneActivation = false;
		}
	}

	public void ChangeScene(string scene) {
		if (scene.Equals(preloadScene)) {
			loadOperation.allowSceneActivation = true;
		} else {
			SceneManager.LoadScene(scene);
		}
	}

	public void ToggleFullscreen() {
		Screen.fullScreen = !Screen.fullScreen;
	}

}
