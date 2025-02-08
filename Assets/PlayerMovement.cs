using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

[RequireComponent(typeof(XRInputActions))]
public class PlayerMovement : MonoBehaviour {
	public Transform playerToMove;
	[Tooltip("The direction that the player should move. Likely to be the transform of the camera.")]
	public Transform playerMoveDirection;

	XRInputActions input;
	TeleportationProvider playerTeleportController;
	const string playerTeleportControllerTag = "Player Teleport Controller";

    void Start() {
		if (playerToMove == null) {
			Debug.LogError("No player assigned to the 'Player To Move' field.");
		}

		input = GetComponent<XRInputActions>();
		playerMoveDirectionWithoutPitch = new("Player Move Direction Holder");
		playerTeleportController = GameObject.FindWithTag(playerTeleportControllerTag).GetComponent<TeleportationProvider>();
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
			  (playerMoveDirectionWithoutPitch.transform.right   * inputDirection.x)
			+ (playerMoveDirectionWithoutPitch.transform.forward * inputDirection.y)
		));

		playerToMove.position += deltaTime * velocity;
	}

	bool previousIsUsingStickMovement;
	void Update() {
		if (previousIsUsingStickMovement != isUsingStickMovement) {
			playerTeleportController.ForceTeleport(playerToMove.position);
			playerTeleportController.enabled = !isUsingStickMovement;
			previousIsUsingStickMovement = isUsingStickMovement;
		}

		if (isUsingStickMovement) {
			UpdateMovement(input.moveDirection);
		}
	}

	public bool isUsingStickMovement = true;
	public void SwitchToStickMovement() => isUsingStickMovement = true;
	public void SwitchToTeleportation() => isUsingStickMovement = false;

	void FixedUpdate() {
		// This also includes the Y.
		velocity *= friction;
	}
}
