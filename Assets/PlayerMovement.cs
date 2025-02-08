using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Transform playerToMove;
    void Start() {
		if (playerToMove == null) {
			Debug.LogError("No player assigned to the 'Player To Move' field.");
		}
    }

	public Vector3 velocity;
	public float moveForce = 100.0f;
	public float friction = 0.85f;
	public Vector2 maxMoveSpeed = new Vector2(20.0f, 20.0f); // TODO

	void UpdateMovement(Vector2 inputDirection) {
		float deltaTime = Time.deltaTime;

		velocity += deltaTime * (moveForce * (
			  (playerToMove.right   * inputDirection.x)
			+ (playerToMove.forward * inputDirection.y)
		));

		playerToMove.position += deltaTime * velocity;
	}

	void Update() {
		UpdateMovement(new Vector2(0.0f, 1.0f));
	}

	void FixedUpdate() {
		velocity.x *= friction;
		velocity.z *= friction;
	}
}
