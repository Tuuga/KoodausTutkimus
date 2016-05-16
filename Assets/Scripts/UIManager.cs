using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {

	public Text counterText;
    public Text orcText;
	public Text buttonText;

	int timesPressed;

    string[] worldTextStrings = { "Yes?", "Hmmm?", "What you want?", "Something need doing?", "Work, work.", "Whaaat?", "Me busy. Leave me alone!!", "No time for play.", "Me not that kind of orc!"};
    int orcCalls;

	void Update () {
        counterText.text = "" + Mathf.Round(Time.time);
    }

	public void ButtonClickCounter () {
		timesPressed++;
		buttonText.text = "Times Pressed: " + timesPressed;
	}

    public void ReloadScene () {
        Scene thisScene = SceneManager.GetActiveScene();
        string thisSceneName = thisScene.name;
        SceneManager.LoadScene(thisSceneName);
    }

    public void WorldButton () {
        orcText.text = worldTextStrings[orcCalls];
        if (orcCalls < worldTextStrings.Length - 1) {
            orcCalls++;
        } else {
            orcCalls = 0;
        }
    }
}
