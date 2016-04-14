using UnityEngine;
using System.Collections;

public class AnimationSwitch : MonoBehaviour {

	public GameObject cube;

	public AnimationClip clipOne;
	public AnimationClip clipTwo;

	Animator anim;

	void Start () {
		anim = cube.GetComponent<Animator>();
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			anim.Play(clipOne.name);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			anim.Play(clipTwo.name);
		}

	}
}
