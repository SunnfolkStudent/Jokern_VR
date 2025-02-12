using UnityEngine;
using UnityEngine.InputSystem;

public class XRInputActions : MonoBehaviour {
	XRIDefaultInputActions actions;

	void Awake()     => actions = new XRIDefaultInputActions();
	void OnEnable()  => actions.Enable();
	void OnDisable() => actions.Disable();

	//[HideInInspector]
	public Vector2 moveDirection { get; private set; }

	void Update() {
		moveDirection = actions.XRILeftLocomotion.Move.ReadValue<Vector2>();
	}
}
