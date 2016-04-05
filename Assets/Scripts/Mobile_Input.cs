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
	public bool useTouch;
	public float accFilterCut;
	public float accUpdateTweak = 1;

	public float forceMultiplier;
	//public GameObject plane;
	//Rigidbody rb;

	float accY;
	float accX;
	Vector3 lowPassValue;

	public Text debugText;
	string debugTextString;

	void Start () {
		//rb = GetComponent<Rigidbody>();
		loggedAcc = new Vector3[logAccAmount];
		for (int i = 0; i < logAccAmount; i++) {
			loggedAcc[i] = Vector3.zero;
		}

		Input.gyro.enabled = true;	
	}

	void FixedUpdate () {
		SetDebugText();
		RotateCube();
	}

	void RotateCube () {
		if (Input.touchCount == 0 && useAccelerometer || !useTouch) { //GyroRot
			SmoothAccelerometer();
		}

		if (Input.touchCount == 1 && useTouch) { // Rotate
			Vector2 touchDelta = Input.touches[0].deltaPosition;
			xParent.transform.Rotate(new Vector3(touchDelta.y, 0, 0));
			yParent.transform.Rotate(new Vector3(0, -touchDelta.x, 0));
		}

		if (Input.touchCount == 2 && useTouch) { // Scale
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

		// transform.localRotation = Quaternion.Euler(loggedAccAv.y * -90, loggedAccAv.x * 90, 0);
		//transform.localPosition = new Vector3(loggedAccAv.x, loggedAccAv.y, -loggedAccAv.z);

		//plane.transform.rotation = Quaternion.Euler(45 * loggedAccAv.y, 0, 45 * -loggedAccAv.x);
		//rb.AddForce(loggedAccAv.x * forceMultiplier * Time.deltaTime, 0, loggedAccAv.y * forceMultiplier * Time.deltaTime);

		
		xParent.transform.localRotation = Quaternion.Euler(loggedAccAv.y * -90, 0, 0);
		yParent.transform.localRotation = Quaternion.Euler(0, loggedAccAv.x * 90, 0);
		
	}

	void SetDebugText() {
		if (Input.touchCount > 0) {
			debugTextString = "Touch Count: " + Input.touchCount;
			for (int i = 0; i < Input.touchCount; i++) {
				debugTextString += "\n Touch " + i;
			}
		} else {
			debugTextString = "Touch Count: 0";
		}
		debugText.text = debugTextString;
	}
}
