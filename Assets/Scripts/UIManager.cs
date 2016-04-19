using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	Text text;
	Text buttonText;

	int timesPressed;

	GameObject box;
	public Material[] rgb;
	Dropdown dd;

	void Start () {
		text = GameObject.Find("Text").GetComponent<Text>();
		buttonText = GameObject.Find("ButtonText").GetComponent<Text>();
		dd = GameObject.Find("Dropdown").GetComponent<Dropdown>();
		box = GameObject.Find("Box");
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

	public void ColorPick () {
		box.GetComponent<MeshRenderer>().material = rgb[dd.value];
	}
}
