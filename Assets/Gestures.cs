using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gestures : MonoBehaviour {

	public Text gestureText;

	public enum AllGestures { SingleTap, TapHold, DoubleTap, SwipeUp, SwipeDown, SwipeLeft, SwipeRight, PinchIn, PinchOut };
	AllGestures currentGesture;
	public AllGestures lastGesture;

	Vector2 touchStartPoint;
	Vector2 touchEndPoint;
	Vector2 touchDir;

	Vector2 touchOnePos;
	Vector2 touchTwoPos;

	float twoTouchLastDist;

	public float tapTime;
	public float doubleTapTime;
	public float gestureCooldown;
	float gestureCooldownTimer;
	bool gestureUsed;

	// <Swipe Data>
	public float minTime = Mathf.Infinity;
	public float avarageTime;
	public float maxTime;
	public float avarageBetweenTime;

	float timesSwiped;
	float allSwipeTimeCombined;
	float allBetweenCombined;
	public float timeBetweenTouch;
	// </Swipe Data>

	float touchTime;

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
			} else if (Input.touchCount == 2) {
				TwoTouchGestures();
			} else {
				touchTime = 0;
				timeBetweenTouch += Time.deltaTime;
			}
		}
		SetGestureText();
	}

	void SetGestureText () {
		
		if (currentGesture == AllGestures.SingleTap) {
			gestureText.text = "Single Tap";
		}
		if (currentGesture == AllGestures.TapHold) {
			gestureText.text = "Touch Time: " + touchTime + "\nTap Hold";
		}
		if (currentGesture == AllGestures.DoubleTap) {
			gestureText.text = "Double Tap";
		}
		if (currentGesture == AllGestures.SwipeUp) {
			gestureText.text = "Touch Time: " + touchTime + "\nSwipe Up\nStart Point: " + touchStartPoint + "\nEnd Point: " + touchEndPoint;
		}
		if (currentGesture == AllGestures.SwipeDown) {
			gestureText.text = "Touch Time: " + touchTime + "\nSwipe Down\nStart Point: " + touchStartPoint + "\nEnd Point: " + touchEndPoint;
		}
		if (currentGesture == AllGestures.SwipeLeft) {
			gestureText.text = "Touch Time: " + touchTime + "\nSwipe Left\nStart Point: " + touchStartPoint + "\nEnd Point: " + touchEndPoint;
		}
		if (currentGesture == AllGestures.SwipeRight) {
			gestureText.text = "Touch Time: " + touchTime + "\nSwipe Right\nStart Point: " + touchStartPoint + "\nEnd Point: " + touchEndPoint;
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
			allBetweenCombined += timeBetweenTouch;
			avarageBetweenTime = allBetweenCombined / timesSwiped;
		}

		if (touchTime > tapTime && Input.GetTouch(0).phase == TouchPhase.Stationary) {
			currentGesture = AllGestures.TapHold;
		}

		if (Input.GetTouch(0).phase == TouchPhase.Ended) {
			timesSwiped++;
			allSwipeTimeCombined += touchTime;
			avarageTime = allSwipeTimeCombined / timesSwiped;
			if (minTime > touchTime) {
				minTime = touchTime;
			}
			if (maxTime < touchTime) {
				maxTime = touchTime;
			}

			if (touchTime < tapTime) {
				currentGesture = AllGestures.SingleTap;
			}
			if (touchTime < tapTime && timeBetweenTouch < doubleTapTime && lastGesture == AllGestures.SingleTap) {
				currentGesture = AllGestures.DoubleTap;
			}

			touchEndPoint = Input.GetTouch(0).position;
			touchDir = (touchEndPoint - touchStartPoint).normalized;

			if (touchDir.x > 0 && (touchDir.x > touchDir.y && touchDir.x > touchDir.y * -1)) {
				currentGesture = AllGestures.SwipeRight;
				touchDir = Vector2.zero;
			}
			if (touchDir.x < 0 && (touchDir.x < touchDir.y && touchDir.x < touchDir.y * -1)) {
				currentGesture = AllGestures.SwipeLeft;
				touchDir = Vector2.zero;
			}
			if (touchDir.y > 0 && (touchDir.y > touchDir.x && touchDir.y > touchDir.x * -1)) {
				currentGesture = AllGestures.SwipeUp;
				touchDir = Vector2.zero;
			}
			if (touchDir.y < 0 && (touchDir.y < touchDir.x && touchDir.y < touchDir.x * -1)) {
				currentGesture = AllGestures.SwipeDown;
				touchDir = Vector2.zero;
			}
			gestureUsed = true;
			lastGesture = currentGesture;
			timeBetweenTouch = 0;
		}
	}
}
