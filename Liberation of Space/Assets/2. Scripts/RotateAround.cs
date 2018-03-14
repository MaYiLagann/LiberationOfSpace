using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

	public Transform CenterTransform;

	public Vector3 Direction;

	public float Speed;
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.RotateAround(CenterTransform.position, Direction, Speed * Time.deltaTime);
	}
}
