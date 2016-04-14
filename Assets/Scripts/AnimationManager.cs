using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {

	public GameObject cube;

	public AnimationClip clipOne;
	public AnimationClip clipTwo;

	public GameObject dude;
	GameObject mainCam;

	Animator anim;

	void Start () {
		anim = cube.GetComponent<Animator>();
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			anim.Play(clipOne.name);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			anim.Play(clipTwo.name);
		}

		dude.transform.LookAt(mainCam.transform);

	}
}
