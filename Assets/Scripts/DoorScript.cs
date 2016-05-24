using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public Animator anim;
	public AnimationClip open;
	public AnimationClip close;
	bool _isOpen;
	public GameObject g;

	void Update () {
		if (Input.GetKeyDown(KeyCode.F)) {
			OpenCloseDoor();
		}	
	}

	void OpenCloseDoor() {
		if (_isOpen) {
			anim.Play(close.name);
		} else {
			anim.Play(open.name);
		}
		_isOpen = !_isOpen;
	}
}
