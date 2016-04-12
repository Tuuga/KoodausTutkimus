using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;


public class SpawnerUIManager : MonoBehaviour {

	public Text text;
	string checkpoint;
	int checkpointInt;
	int balls;
	float fps;
	

	void Update () {
		if (balls >= 250 && checkpointInt == 0) {
			SetCheckpoint();
			checkpointInt++;
		} else if (balls >= 500 && checkpointInt == 1) {
			SetCheckpoint();
			checkpointInt++;
		} else if (balls >= 750 && checkpointInt == 2) {
			SetCheckpoint();
			checkpointInt++;
		} else if (balls >= 1000 && checkpointInt == 3) {
			SetCheckpoint();
			checkpointInt++;
		} else if (balls >= 1500 && checkpointInt == 4) {
			SetCheckpoint();
			checkpointInt++;
		} else if (balls >= 2000 && checkpointInt == 5) {
			SetCheckpoint();
			checkpointInt++;
		} else if (balls >= 3000 && checkpointInt == 6) {
			SetCheckpoint();
			checkpointInt++;
		}
	}

	public void SetText (int ballAmount) {
		text.text = "Balls: " + ballAmount + checkpoint;
		balls = ballAmount;
	}

	void SetCheckpoint () {
		checkpoint += "\nFPS: " + fps + " Balls: " + balls;
	}
	public void SetFps (float newFps) {
		fps = newFps;
		if (fps < 4f) {
			EditorApplication.isPaused = true;
		}
	}
}
