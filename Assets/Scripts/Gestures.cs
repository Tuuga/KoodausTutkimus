using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gestures : MonoBehaviour {

	public Text gestureText;
	public LineRenderer line;

	public enum AllGestures { SingleTap, TapHold, DoubleTap, SwipeUp, SwipeDown, SwipeLeft, SwipeRight, PinchIn, PinchOut };
	AllGestures currentGesture;
	public AllGestures lastGesture;

	Vector2 touchStartPoint;
	Vector2 touchLastPoint;
	Vector2 swipeDir;

	public float swipeDeadZone;
	float swipeDist;

	Vector2 touchOnePos;
	Vector2 touchTwoPos;

	float twoTouchLastDist;

	public float tapTime;
	public float doubleTapTime;
	public float gestureCooldown;
	float gestureCooldownTimer;
	bool gestureUsed;

	// <Swipe Data> SD
	/*
	public float minTime = Mathf.Infinity;
	public float avarageTime;
	public float maxTime;
	public float avarageBetweenTime;

	float timesSwiped;
	float allSwipeTimeCombined;
	float allBetweenCombined;
	public float timeBetweenTouch;
	*/
	// </Swipe Data>

	float touchTime;

	Camera mainCam;

	void Start () {
		mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	void Update () {
		if (gestureUsed) {
			gestureCooldownTimer += Time.deltaTime;
			if (gestureCooldownTimer > gestureCooldown) {
				gestureUsed = false;
				gestureCooldownTimer = 0;
			}
		}

		if (!gestureUsed) {
			if (Input.touchCount == 1) {
				touchTime += Time.deltaTime;
				OneTouchGestures();
				SetLine(touchStartPoint, touchLastPoint);
			} else if (Input.touchCount == 2) {
				TwoTouchGestures();
				SetLine(touchOnePos, touchTwoPos);
			} else {
				touchTime = 0;
				//timeBetweenTouch += Time.deltaTime;
			}
		}
		SetGestureText();
	}

	void SetLine (Vector2 s, Vector2 e) {
		Vector3 sToWorld = mainCam.ScreenToWorldPoint(new Vector3 (s.x, s.y, mainCam.nearClipPlane + 0.1f));
		Vector3 eToWorld = mainCam.ScreenToWorldPoint(new Vector3(e.x, e.y, mainCam.nearClipPlane + 0.1f));

		line.SetPosition(0, sToWorld);
		line.SetPosition(1, eToWorld);
	}

	void SetGestureText () {
		
		if (currentGesture == AllGestures.SingleTap) {
			gestureText.text = "Single Tap";
		}
		if (currentGesture == AllGestures.TapHold) {
			gestureText.text = "Touch Time: " + touchTime + "\nTap Hold";
		}
		if (currentGesture == AllGestures.SwipeUp) {
			gestureText.text = "Touch Time: " + touchTime + "\nSwipe Up\nStart Point: " + touchStartPoint + "\nEnd Point: " + touchLastPoint;
		}
		if (currentGesture == AllGestures.SwipeDown) {
			gestureText.text = "Touch Time: " + touchTime + "\nSwipe Down\nStart Point: " + touchStartPoint + "\nEnd Point: " + touchLastPoint;
		}
		if (currentGesture == AllGestures.SwipeLeft) {
			gestureText.text = "Touch Time: " + touchTime + "\nSwipe Left\nStart Point: " + touchStartPoint + "\nEnd Point: " + touchLastPoint;
		}
		if (currentGesture == AllGestures.SwipeRight) {
			gestureText.text = "Touch Time: " + touchTime + "\nSwipe Right\nStart Point: " + touchStartPoint + "\nEnd Point: " + touchLastPoint;
		}
		if (currentGesture == AllGestures.PinchIn) {
			gestureText.text = "Touch Dist: " + twoTouchLastDist + "\nPinch In\nTouch One: " + touchOnePos + "\nTouch Two: " + touchTwoPos;
		}
		if (currentGesture == AllGestures.PinchOut) {
			gestureText.text = "Touch Dist: " + twoTouchLastDist + "\nPinch Out\nTouch One: " + touchOnePos + "\nTouch Two: " + touchTwoPos;
		}
	}

	void TwoTouchGestures () {

		touchOnePos = Input.GetTouch(0).position;
		touchTwoPos = Input.GetTouch(1).position;
		float currentDist = (touchOnePos - touchTwoPos).magnitude;

		if (Input.GetTouch(0).phase != TouchPhase.Stationary || Input.GetTouch(1).phase != TouchPhase.Stationary) {
			if (currentDist > twoTouchLastDist) {
				currentGesture = AllGestures.PinchOut;
			} else {
				currentGesture = AllGestures.PinchIn;
			}
		}

		if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(1).phase == TouchPhase.Ended) {
			gestureUsed = true;
			lastGesture = currentGesture;
		}

		twoTouchLastDist = (touchOnePos - touchTwoPos).magnitude;
	}

	void OneTouchGestures () {

		if (Input.GetTouch(0).phase == TouchPhase.Began) {
			touchStartPoint = Input.GetTouch(0).position;
			// SD
			//allBetweenCombined += timeBetweenTouch;
			//avarageBetweenTime = allBetweenCombined / timesSwiped;
		}

		if (touchTime > tapTime && Input.GetTouch(0).phase == TouchPhase.Stationary && swipeDist < 50f) {
			currentGesture = AllGestures.TapHold;
			print("TapHold SwipeDist: " + swipeDist);
		}

		if (Input.GetTouch(0).phase == TouchPhase.Ended) {
			
			// SD
			/*
			timesSwiped++;
			allSwipeTimeCombined += touchTime;
			avarageTime = allSwipeTimeCombined / timesSwiped;
			if (minTime > touchTime) {
				minTime = touchTime;
			}
			if (maxTime < touchTime) {
				maxTime = touchTime;
			}
			*/

			if (touchTime < tapTime) {
				currentGesture = AllGestures.SingleTap;
			}

			// --

			if (swipeDist > swipeDeadZone) {

				if (swipeDir.x > 0 && (swipeDir.x > swipeDir.y && swipeDir.x > swipeDir.y * -1)) {
					currentGesture = AllGestures.SwipeRight;
					swipeDir = Vector2.zero;
				}
				if (swipeDir.x < 0 && (swipeDir.x < swipeDir.y && swipeDir.x < swipeDir.y * -1)) {
					currentGesture = AllGestures.SwipeLeft;
					swipeDir = Vector2.zero;
				}
				if (swipeDir.y > 0 && (swipeDir.y > swipeDir.x && swipeDir.y > swipeDir.x * -1)) {
					currentGesture = AllGestures.SwipeUp;
					swipeDir = Vector2.zero;
				}
				if (swipeDir.y < 0 && (swipeDir.y < swipeDir.x && swipeDir.y < swipeDir.x * -1)) {
					currentGesture = AllGestures.SwipeDown;
					swipeDir = Vector2.zero;
				}
				print(currentGesture + " Swipe Dist: " + swipeDist);
			}

			gestureUsed = true;
			lastGesture = currentGesture;
			// SD
			//timeBetweenTouch = 0;
		}
		// --
		touchLastPoint = Input.GetTouch(0).position;
		swipeDir = (touchLastPoint - touchStartPoint).normalized;
		swipeDist = (touchLastPoint - touchStartPoint).magnitude;
	}
}
