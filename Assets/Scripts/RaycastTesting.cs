using UnityEngine;
using System.Collections;

public class RaycastTesting : MonoBehaviour {

	public LayerMask mask;
	public float speed;

	GameObject _hitTarget;

	bool _hit;
	float damageTimer;
	float _hitDist;

	void Update () {

		Controls();

		if (Input.GetKey(KeyCode.Space)) {
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, mask.value);
			if (hit.collider != null) {
				_hitTarget = hit.collider.gameObject;
				_hit = true;
				_hitDist = hit.distance;
			}
		}

		if (_hit) {
			_hitTarget.GetComponent<SpriteRenderer>().color = Color.red;
			damageTimer += Time.deltaTime;
			Debug.DrawLine(transform.position, transform.position + Vector3.right * _hitDist, Color.red);
			if (damageTimer > 0.2f) { //HARDCODE
				damageTimer = 0;
				_hit = false;
				_hitTarget.GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}

	void Controls () {
		if (Input.GetKey(KeyCode.W)) {
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S)) {
			transform.position -= Vector3.up * speed * Time.deltaTime;
		}
	}
}
