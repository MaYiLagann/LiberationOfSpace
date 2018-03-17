using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildItemMove : MonoBehaviour {

	public string RaycastTargetTag = "BuildRayTarget";

	public int GridDistance = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		if(Physics.Raycast(ray, out hit)) {
			Vector3 point = hit.point;
			point.x = Mathf.Round(point.x / GridDistance) * GridDistance;
			point.y = 0f;
			point.z = Mathf.Round(point.z / GridDistance) * GridDistance;
			gameObject.transform.position = point;
		}
	}
}
