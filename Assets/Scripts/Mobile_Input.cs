using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mobile_Input : MonoBehaviour {

	float accUpdateInterval = 1 / 60;
	public float accUpdateTweak = 1;
	float lowPassFactor;
	Vector3 lowPassValue;

	public Text debugText;
	string debugTextString;

	void Start () {
		Input.gyro.enabled = true;
		lowPassFactor = accUpdateInterval / accUpdateTweak;		
	}

	void Update () {
		SetDebugText();
		RotateCube();
	}

	void RotateCube () {
		if (Input.touchCount == 0) { //GyroRot

			float accY = Mathf.Lerp(lowPassValue.y, Input.acceleration.y, lowPassFactor);
			float accX = Mathf.Lerp(lowPassValue.x, Input.acceleration.x, lowPassFactor);

			transform.localRotation = Quaternion.Euler(accY * 90, accX * -90, 0);
			lowPassValue = Input.acceleration;
		}

		if (Input.touchCount == 1) { // Rotate
			Vector2 touchDelta = Input.touches[0].deltaPosition;
			transform.Rotate(new Vector3(0, 0, touchDelta.y));
			transform.parent.Rotate(new Vector3(0, -touchDelta.x, 0));
		}
		if (Input.touchCount == 2) { // Scale
			transform.localScale = Vector3.one * ((Input.touches[0].position - Input.touches[1].position).magnitude / 220);
		}
	}

	void SetDebugText() {
		if (Input.touchCount > 0) {
			debugTextString = "";
			for (int i = 0; i < Input.touchCount; i++) {
				debugTextString += "\n Touch " + i;
			}
		} else {
			debugTextString = "";
		}
		debugText.text = debugTextString;
	}
}
