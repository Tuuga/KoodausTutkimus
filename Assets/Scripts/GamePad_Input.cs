using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePad_Input : MonoBehaviour {

	public GameObject indicatorOne;
	public GameObject indicatorTwo;
	public GameObject indicatorThree;

	Camera mainCam;

	public Text inputOneText;
	public Text inputTwoText;
	public Text inputThreeText;

	Vector3 lastMousePos;

	void Start () {
		mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	void Update () {
		Vector3 indicatorOnePos = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
		Vector3 indicatorTwoPos = new Vector3(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"), 0f);
		Vector3 indicatorThreePos = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f);


		indicatorOne.transform.position = indicatorOnePos;
		indicatorTwo.transform.position = indicatorTwoPos;


		// if left mouse button held, mouse acts as a pointer
		// else acts as mouse delta (first person controls)
		if (Input.GetKey(KeyCode.Mouse0)) {
			Vector3 newThreePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
			newThreePos.z = 0;
			indicatorThree.transform.position = newThreePos;
		} else {
			indicatorThree.transform.position = indicatorThreePos;
		}

        float x1 = Mathf.Round(Input.GetAxis("Horizontal") * 10f) / 10;
		float y1 = Mathf.Round(Input.GetAxis("Vertical") * 10f) / 10;

		float x2 = Mathf.Round(Input.GetAxis("Horizontal2") * 10f) / 10;
		float y2 = Mathf.Round(Input.GetAxis("Vertical2") * 10f) / 10;

		float x3 = Mathf.Round(indicatorThree.transform.position.x * 10f) / 10;
		float y3 = Mathf.Round(indicatorThree.transform.position.y * 10f) / 10;


		inputOneText.text = "X: " + x1 + " Y: " + y1;
		inputTwoText.text = "X: " + x2 + " Y: " + y2;
		inputThreeText.text = "X: " + x3 + " Y: " + y3;


		// Gamepad buttons
		// Gamepad 1
		if (Input.GetKey(KeyCode.Joystick1Button0)) {
			print("1 A");
		}
		if (Input.GetKey(KeyCode.Joystick1Button1)) {
			print("1 B");
		}
		if (Input.GetKey(KeyCode.Joystick1Button2)) {
			print("1 X");
		}
		if (Input.GetKey(KeyCode.Joystick1Button3)) {
			print("1 Y");
		}

		// Gamepad 2
		if (Input.GetKey(KeyCode.Joystick2Button0)) {
			print("2 A");
		}
		if (Input.GetKey(KeyCode.Joystick2Button1)) {
			print("2 B");
		}
		if (Input.GetKey(KeyCode.Joystick2Button2)) {
			print("2 X");
		}
		if (Input.GetKey(KeyCode.Joystick2Button3)) {
			print("2 Y");
		}
	}
}
