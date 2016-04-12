using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	Text text;

	void Start () {
		text = GameObject.Find("Text").GetComponent<Text>();
	}

	void Update () {
		if (Input.inputString != "") {
			text.text = Input.inputString;
		}
	}
}
