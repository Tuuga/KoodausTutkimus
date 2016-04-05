using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadSceneManager : MonoBehaviour {

	GameObject[] sceneButtons;
	
	void Start () {
		sceneButtons = GameObject.FindGameObjectsWithTag("SceneButton");
		ShowSceneButtons(); // Called so buttons are off by default
		print(sceneButtons.Length);
	}

	public void ShowSceneButtons () {
		for (int i = 0; i < sceneButtons.Length; i++) {
			sceneButtons[i].SetActive(!sceneButtons[i].activeSelf);
		}
	}

	public void LoadTouchScene () {
		SceneManager.LoadScene("Touch");
	}

	public void LoadAccelerometerScene () {
		SceneManager.LoadScene("Accelerometer");
	}
}
