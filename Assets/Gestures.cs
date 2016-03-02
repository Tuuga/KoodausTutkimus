using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gestures : MonoBehaviour {

	public Text gestureText;

	enum AllGestures { SwipeUp, SwipeDown, SwipeLeft, SwipeRight, PinchIn, PinchOut };
	AllGestures currentGesture;

	Vector2 touchStartPoint;
	Vector2 touchEndPoint;
	Vector2 touchDir;

	Vector2 touchOnePos;
	Vector2 touchTwoPos;

	float twoTouchLastDist;

	float touchTime;

	void Update () {
		if (Input.touchCount == 1) {
			OneTouchGestures();
			touchTime += Time.deltaTime;
		} else if (Input.touchCount == 2) {
			TwoTouchGestures();
		} else {
			touchTime = 0;
		}

		SetGestureText();
	}

	void SetGestureText () {
		
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

		twoTouchLastDist = (touchOnePos - touchTwoPos).magnitude;
	}

	void OneTouchGestures () {

		if (Input.GetTouch(0).phase == TouchPhase.Began) {
			touchStartPoint = Input.GetTouch(0).position;
		}
		
		if (Input.GetTouch(0).phase == TouchPhase.Ended) {
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
		}
	}
}
