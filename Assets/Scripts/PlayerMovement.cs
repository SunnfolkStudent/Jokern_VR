using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

[RequireComponent(typeof(XRInputActions))]
public class PlayerMovement : MonoBehaviour {
	public Transform playerTransform;
	[Tooltip("The direction that the player should move. Likely to be the transform of the camera.")]
	public Transform playerMoveDirection;
	public CharacterController playerCharacterController;

	XRInputActions input;
	TeleportationProvider playerTeleportController;
	const string playerTeleportControllerTag = "Player Teleport Controller";
	

    void Start() {
		if (playerTransform == null) {
			Debug.LogError("No player assigned to the 'Player To Move' field.");
		}

		input = GetComponent<XRInputActions>();
		playerMoveDirectionWithoutPitch = new("Player Move Direction Holder");
		var playerTeleportControllerGameObject = GameObject.FindWithTag(playerTeleportControllerTag);
		if (playerTeleportControllerGameObject == null) {
			Debug.LogError($"Could not find an object with the '{playerTeleportControllerTag}' tag.");
		}else {
			playerTeleportController = playerTeleportControllerGameObject.GetComponent<TeleportationProvider>();
		}
    }

	Vector3 velocity;
	public float moveForce = 10.0f;
	public float friction = 0.85f;
	public float gravity = 10.0f;

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

		if (playerCharacterController.isGrounded) {
			velocity.y = 0.0f;
		} else {
			velocity.y -= gravity * Time.deltaTime;
		}

		playerCharacterController.Move(deltaTime * velocity);
	}

	bool previousIsUsingStickMovement;
	void Update() {
		if (previousIsUsingStickMovement != isUsingStickMovement) {
			if (playerTeleportController != null) {
				playerTeleportController.ForceTeleport(playerTransform.position);
				playerTeleportController.enabled = !isUsingStickMovement;
			}
			
			previousIsUsingStickMovement = isUsingStickMovement;
		}

		if (isUsingStickMovement) {
			UpdateMovement(input.moveDirection);
		}
	}

	public static bool isUsingStickMovement = true;
	public static void SwitchToStickMovement() => isUsingStickMovement = true;
	public static void SwitchToTeleportation() => isUsingStickMovement = false;

	void FixedUpdate() {
		velocity.x *= friction;
		velocity.z *= friction;
	}
}