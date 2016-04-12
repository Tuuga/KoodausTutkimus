using UnityEngine;
using System.Collections;

public class CameraAnimationScript : MonoBehaviour {

	public Transform focus;
	public GameObject dude;
	Animator cameraAnim;
	public float smooth;
	public float animationFloatTest;

	bool playAnimation;

	void Start () {
		cameraAnim = GetComponent<Animator>();
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.Space)) {
			playAnimation = true;
			cameraAnim.SetBool("Start", true);
		}

		if (playAnimation == true) {
			Quaternion lookRot = Quaternion.LookRotation(focus.position - transform.position);
			transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * smooth);
		}
		dude.transform.LookAt(transform);
	}

	// Called at the of the animation
	public void SetStartFalse () {
		cameraAnim.SetBool("Start", false);
		playAnimation = false;
	}
}
