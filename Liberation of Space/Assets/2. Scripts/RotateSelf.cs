using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour {

	public Vector3 Direction;

	public float Speed;
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate(Direction * Speed * Time.deltaTime);
	}
}
