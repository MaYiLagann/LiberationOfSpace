using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelf : MonoBehaviour {

	public Vector3 MoveSpeed;

	[Header("Rotate Settings")]
	public Vector2 RotateSpeed;

	public Vector2 MaxAngle;
	public Vector2 MinAngle;

	private Vector3 currentRotation = Vector3.zero;

	void Start () {
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		// move
		gameObject.transform.Translate(new Vector3(
			Input.GetAxis("Horizontal") * MoveSpeed.x,
			0f,
			Input.GetAxis("Vertical") * MoveSpeed.z) * Time.deltaTime
		);

		gameObject.transform.position += new Vector3(0f, Input.GetAxis("Jump") * MoveSpeed.y - Input.GetAxis("Duck") * MoveSpeed.y, 0f) * Time.deltaTime;

		// rotate
		currentRotation += new Vector3(-Input.GetAxis("Mouse Y") * RotateSpeed.x, Input.GetAxis("Mouse X") * RotateSpeed.y, 0f) * Time.deltaTime;
		currentRotation.x = MaxAngle.y != 0 && currentRotation.x > MaxAngle.y ? MaxAngle.y : currentRotation.x;
		currentRotation.x = MinAngle.y != 0 && currentRotation.x < MinAngle.y ? MinAngle.y : currentRotation.x;
		currentRotation.y = MaxAngle.x != 0 && currentRotation.y > MaxAngle.x ? MaxAngle.x : currentRotation.y;
		currentRotation.y = MinAngle.x != 0 && currentRotation.y < MinAngle.x ? MinAngle.x : currentRotation.y;
		gameObject.transform.eulerAngles = currentRotation;
	}
}
