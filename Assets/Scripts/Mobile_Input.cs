using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mobile_Input : MonoBehaviour {

	int logIndex;
	public int logAccAmount;
	Vector3 loggedAccAv;
	Vector3[] loggedAcc;

	public GameObject yParent;
	public GameObject xParent;

	public bool useAccelerometer;
	public float accFilterCut;
	public float accUpdateTweak = 1;
	float accY;
	float accX;
	Vector3 lowPassValue;

	public Text debugText;
	string debugTextString;

	void Start () {
		loggedAcc = new Vector3[logAccAmount];
		for (int i = 0; i < logAccAmount; i++) {
			loggedAcc[i] = Vector3.zero;
		}

		Input.gyro.enabled = true;	
	}

	void Update () {
		SetDebugText();
		RotateCube();
	}

	void RotateCube () {
		if (Input.touchCount == 0 && useAccelerometer) { //GyroRot

			SmoothAccelerometer();
			/*		// Attempt at a lowpass filter
			accY = Mathf.Lerp(lowPassValue.y * 90, Input.acceleration.y *	90, accUpdateTweak);
			accX = Mathf.Lerp(lowPassValue.x * -90, Input.acceleration.x * -90, accUpdateTweak);

			xParent.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(accY, 0, 0), Quaternion.Euler(Input.acceleration), accUpdateTweak);
			yParent.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, accX, 0), Quaternion.Euler(Input.acceleration), accUpdateTweak);

			if (lowPassValue.magnitude - Input.acceleration.magnitude > accFilterCut || lowPassValue.magnitude - Input.acceleration.magnitude < -accFilterCut) {
				lowPassValue = Input.acceleration;
			}
			*/
		}

		if (Input.touchCount == 1) { // Rotate
			Vector2 touchDelta = Input.touches[0].deltaPosition;
			xParent.transform.Rotate(new Vector3(touchDelta.y, 0, 0));
			yParent.transform.Rotate(new Vector3(0, -touchDelta.x, 0));
		}

		if (Input.touchCount == 2) { // Scale
			transform.localScale = Vector3.one * ((Input.touches[0].position - Input.touches[1].position).magnitude / 220);
		}
	}

	void SmoothAccelerometer () {
		loggedAcc[logIndex] = Input.acceleration;
		logIndex++;
		if (logIndex == logAccAmount) {
			logIndex = 0;
		}
		for (int i = 0; i < logAccAmount; i++) {
			loggedAccAv += loggedAcc[i];
		}
		loggedAccAv /= logAccAmount;

		xParent.transform.localRotation = Quaternion.Euler(loggedAccAv.y * -90, 0, 0);
		yParent.transform.localRotation = Quaternion.Euler(0, loggedAccAv.x * 90, 0);
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
