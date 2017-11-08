using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour {
	public Transform target;
	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;
	private Vector3 TargetPos = Vector3.zero;
	private Vector3 DefaultDistance = Vector3.zero;
	private string MainObjName = "MainCube" ;

	Transform TTF;

	// Use this for initialization
	void Start () {
		TTF = GameObject.Find(MainObjName).transform;
		DefaultDistance = Camera.main.transform.position - TTF.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			//shot a ray from Camera
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if(Physics.Raycast(ray,out hitInfo)){
				Debug.DrawLine(ray.origin,hitInfo.point);
				GameObject gameObj = hitInfo.collider.gameObject;

				bool ischild = false;
				foreach (Transform child in TTF.transform) {
					if (child == gameObj.transform) {
						ischild = true;
						break;
					}
				}

				Debug.Log (ischild);
				if (ischild) {
					TTF = gameObj.transform;
					//Debug.Log ("Is Child");
				} else {
					//Debug.Log ("Not Child");
				}
			}
		} else if(Input.GetMouseButtonDown (1)) {
			TTF = TTF.parent ? TTF.parent : TTF;
		}

		// move target //
		Vector3 target = TTF.position + DefaultDistance;
		if (target != this.transform.position) {
			this.transform.position = Vector3.SmoothDamp (this.transform.position, target, ref velocity,  smoothTime);
			Debug.Log (target);
		}
	}
}

