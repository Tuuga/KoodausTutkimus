using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	public Text text;
    public Text worldText;
	public Text buttonText;

	int timesPressed;

    string[] worldTextStrings = { "Yes?", "Hmmm?", "What you want?", "Something need doing?", "Work, work.", "Whaaat?", "Me busy. Leave me alone!!", "No time for play.", "Me not that kind of orc!"};
    int orcCalls;

	void Start () {
		
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

    public void WorldButton () {
        worldText.text = worldTextStrings[orcCalls];
        if (orcCalls < worldTextStrings.Length - 1) {
            orcCalls++;
        } else {
            orcCalls = 0;
        }
    }
}
