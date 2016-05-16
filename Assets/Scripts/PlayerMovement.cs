using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using UnityStandardAssets.ImageEffects;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed;
	public float sprintSpeed;
	public float mouseSens;
	public float upDownRange;
    public float health = 100f;
    public float maxImmune;
    public Image hpBar;
    public float stepDistance;

	float _verticalRotation;
	float _horizontalRotation;
    float _immuneTime;
    float _sprinting = 0;

    bool _mouseLock;

	[HideInInspector]
	public bool gameOver;

	GameObject _mainCam;
	Transform _mainCamPos;
    //ScreenOverlay _sc;

	Quaternion _baseRotation = Quaternion.identity;

    string[] _steps = new string[4] {"step1", "step2", "step3", "step4" };
    float _step;
    
	Rigidbody _rb;

	void Start () {
		MouseLock();
		_rb = GetComponent<Rigidbody>();
		_mainCam = GameObject.FindGameObjectWithTag("MainCamera");
		_mainCamPos = GameObject.Find("MainCamPos").transform;
        //_sc = _mainCam.GetComponentInChildren<ScreenOverlay>();
	}

	void Update () {
		if (_mouseLock && !gameOver)
			MouseRotate();

        if (Input.GetKeyDown(KeyCode.LeftAlt))
            MouseLock();

        //hpBar.fillAmount = health / 100f;

        if (_immuneTime > 0) {
            _immuneTime = Mathf.Max(0, _immuneTime - Time.deltaTime);
            //_sc.intensity = 3 * _immuneTime / maxImmune;
        }
	}

	void FixedUpdate () {
		if (!gameOver)
			Movement();
	}

    void LateUpdate() {
        _mainCam.transform.position = _mainCamPos.position;
    }

	void MouseRotate () {
		_verticalRotation -= Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
		_verticalRotation = Mathf.Clamp(_verticalRotation, -upDownRange, upDownRange);

		_horizontalRotation += Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;

		_mainCam.transform.rotation = Quaternion.Euler(_verticalRotation, _horizontalRotation, 0) * _baseRotation;
		transform.localRotation = Quaternion.Euler(0 , _horizontalRotation, 0) * _baseRotation;
        _rb.angularVelocity = Vector3.zero;
	}

	void MouseLock () {
		_mouseLock = !_mouseLock;
		if (_mouseLock) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	void Movement () {
        Vector3 prev = _rb.position;
		Vector3 moveDir = Vector3.zero;
		float newMoveSpeed = moveSpeed;

		moveDir += transform.forward * Input.GetAxis("Vertical");
		moveDir += transform.right * Input.GetAxis("Horizontal");

			if (Input.GetAxis("Sprint") > 0 && moveDir != Vector3.zero && Input.GetAxis("Vertical") > 0f) {
				newMoveSpeed = sprintSpeed;
				_sprinting = 0.25f;
			} else if (_sprinting > 0) {
				_sprinting -= Time.deltaTime;
			}

		moveDir = new Vector3 (moveDir.x, 0, moveDir.z).normalized;
		_rb.velocity = Vector3.zero;
		//float movePosX = Mathf.Clamp((transform.position + (moveDir * newMoveSpeed * Time.deltaTime)).x, -29f, 29f);
		//float movePosZ = Mathf.Clamp((transform.position + (moveDir * newMoveSpeed * Time.deltaTime)).z, -29f, 29f);
        float movePosX = (transform.position + (moveDir * newMoveSpeed * Time.deltaTime)).x;
        float movePosZ = (transform.position + (moveDir * newMoveSpeed * Time.deltaTime)).z;
        Vector3 movePos = new Vector3 (movePosX, 0, movePosZ);
		_rb.MovePosition(movePos);
        if (_rb.constraints != (RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ))
        _step += (_rb.position - prev).magnitude;

        if (_step > stepDistance) {
            _step -= stepDistance;
            //AudioMaster.Play(_steps[Random.Range(0, 4)]);
        }
		//transform.position += moveDir * newMoveSpeed * Time.deltaTime;
	}

	void OnCollisionEnter (Collision c) {
        if (c.gameObject.tag == "BossBall" && _immuneTime == 0 && !gameOver) {
            //GetHit(BossStateManager.Damage());
        }
	}

    void GetHit(float dmg) {
        if (dmg == 0)
            return;

        //AudioMaster.Play("hurt");
        //CameraShaker.Shake(0.03f*dmg,maxImmune/10f*dmg);
        health -= dmg;
        _immuneTime = maxImmune;
    }

    public bool IsSprinting() {
        return (_sprinting > 0);
    }

    public float Sprint() {
        return _sprinting;
    }
}
