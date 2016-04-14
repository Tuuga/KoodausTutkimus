using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	Text text;
	Text buttonText;

	int timesPressed;

	void Start () {
		text = GameObject.Find("Text").GetComponent<Text>();
		buttonText = GameObject.Find("ButtonText").GetComponent<Text>();
	}

	void Update () {

		if (Input.inputString != "") {
			text.text = Input.inputString;
		}
	}

	public void ButtonClickCounter () {
		timesPressed++;
		buttonText.text = "Times Pressed: " + timesPressed;
	}
}
