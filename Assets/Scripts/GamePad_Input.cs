using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePad_Input : MonoBehaviour {

	public GameObject indicatorOne;
	public GameObject indicatorTwo;

	public Text inputOneText;
	public Text inputTwoText;

	void Update () {
		Vector3 indicatorOnePos = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
		Vector3 indicatorTwoPos = new Vector3(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"), 0f);
		indicatorOne.transform.position = indicatorOnePos;
		indicatorTwo.transform.position = indicatorTwoPos;


		float x1 = Mathf.Round(Input.GetAxis("Horizontal") * 10f) / 10;
		float y1 = Mathf.Round(Input.GetAxis("Vertical") * 10f) / 10;
		float x2 = Mathf.Round(Input.GetAxis("Horizontal2") * 10f) / 10;
		float y2 = Mathf.Round(Input.GetAxis("Vertical2") * 10f) / 10;


		inputOneText.text = "X: " + x1 + " Y: " + y1;
		inputTwoText.text = "X: " + x2 + " Y: " + y2;
	}
}
