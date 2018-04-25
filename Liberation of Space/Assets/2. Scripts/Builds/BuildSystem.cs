using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour {

	[Header("Raycast Settings")]
	public string RaycastTargetLayer = "BuildRayTarget";
	public GameObject PlaceMarkObject;

	[Header("Placement Settings")]
	public int GridDistance = 1;
	public GameObject[] PartObjects;

	// placement system values
	private int currentPartId = 0;
	private List<GameObject> objectParts = new List<GameObject>();
	private Vector3 latestHitPoint;
	private GameObject hitObjectPart;
	private int hitLayer;

	// input system values
	private bool inputPlace = false;
	private bool inputRemove = false;

	void Awake () {
		hitLayer = LayerMask.GetMask(RaycastTargetLayer);
	}
	
	void Update () {
		Raycast((hitpoint) => {
			PlaceMarkObject.transform.position = hitpoint;
			latestHitPoint = hitpoint;
		}, out hitObjectPart);

		if(Input.GetAxis("Fire1") == 1f) {
			if(inputPlace) 
				PlaceObject(latestHitPoint, Vector3.zero);
			inputPlace = false;
		} else 
			inputPlace = true;

		if(Input.GetAxis("Fire2") == 1f) {
			if(inputRemove && hitObjectPart != null) 
				RemoveObject(hitObjectPart);
			inputRemove = false;
		} else
			inputRemove = true;
	}

	void PlaceObject(Vector3 position, Vector3 rotation) {
		BuildObjectPart newpart = Instantiate(PartObjects[currentPartId]).AddComponent<BuildObjectPart>();
		newpart.index = objectParts.Count;
		newpart.transform.position = position;
		newpart.transform.rotation = Quaternion.Euler(rotation);
		objectParts.Add(newpart.gameObject);
	}

	void RemoveObject(GameObject target) {
		GameObject obj = objectParts[target.GetComponent<BuildObjectPart>().index];
		objectParts.RemoveAt(target.GetComponent<BuildObjectPart>().index);
		Destroy(obj);
	}

	void Raycast(Action<Vector3> hitFunction, out GameObject hitobject) {
		hitobject = null;
		RaycastHit hit;
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		if(Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayer)) {
			Vector3 point = hit.point;
			point.x = Mathf.Round(point.x / GridDistance) * GridDistance;
			point.y = Mathf.Round(point.y / GridDistance) * GridDistance;
			point.z = Mathf.Round(point.z / GridDistance) * GridDistance;

			BuildObjectPart part = hit.transform.gameObject.GetComponentInParent<BuildObjectPart>();
			if(hit.transform.gameObject.layer == LayerMask.NameToLayer(RaycastTargetLayer) && part != null)
				hitobject = part.transform.gameObject;

			hitFunction(point);
		}
	}
}
