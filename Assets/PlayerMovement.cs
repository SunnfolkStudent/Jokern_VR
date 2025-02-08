using UnityEngine;

[RequireComponent(typeof(XRInputActions))]
public class PlayerMovement : MonoBehaviour {
	public Transform playerToMove;
	[Tooltip("The direction that the player should move. Likely to be the transform of the camera.")]
	public Transform playerMoveDirection;

	XRInputActions input;

    void Start() {
		if (playerToMove == null) {
			Debug.LogError("No player assigned to the 'Player To Move' field.");
		}

		input = GetComponent<XRInputActions>();
		playerMoveDirectionWithoutPitch = new("Player Move Direction Holder");
    }

	Vector3 velocity;
	public float moveForce = 10.0f;
	public float friction = 0.85f;

	GameObject playerMoveDirectionWithoutPitch;

	void UpdateMovement(Vector2 inputDirection) {
		float deltaTime = Time.deltaTime;

		var newLocalEulerAngles = playerMoveDirection.localEulerAngles;
		newLocalEulerAngles.x = 0.0f;
		newLocalEulerAngles.z = 0.0f;
		playerMoveDirectionWithoutPitch.transform.localEulerAngles = newLocalEulerAngles;

		velocity += deltaTime * (moveForce * (
			  (playerMoveDirection.right   * inputDirection.x)
			+ (playerMoveDirection.forward * inputDirection.y)
		));

		playerToMove.position += deltaTime * velocity;
	}

	void Update() {
		UpdateMovement(input.moveDirection);
	}

	void FixedUpdate() {
		// This also includes the Y.
		velocity *= friction;
	}
}
