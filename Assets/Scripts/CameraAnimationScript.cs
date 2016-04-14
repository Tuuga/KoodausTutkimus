using UnityEngine;
using System.Collections;

public class CameraAnimationScript : MonoBehaviour {

	public Transform target;
	
	Animator cameraAnim;
	public float smooth;
	public float animationFloatTest;
	bool lookAtTarget;

	void Start () {
		cameraAnim = GetComponent<Animator>();
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.Space)) {
			cameraAnim.Play("CameraAnimation");
			lookAtTarget = true;
		}

		if (lookAtTarget == true) {
			
			Quaternion lookRot = Quaternion.LookRotation(target.position - transform.position);
			transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * smooth);
			
			//LookAt jitters with animations
			//transform.LookAt(focus);
		}
	}

	// Called at the of the animation
	public void AnimationEnd () {
		lookAtTarget = false;
		cameraAnim.Play("Idle");
	}
}
