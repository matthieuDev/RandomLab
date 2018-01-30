using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headMovement : MonoBehaviour {
	
	Transform head;
	Transform body;
	void Start () {
		head = transform.parent;
		body = head.parent;
	}

	// Update is called once per frame
	void Update () {


		Vector3 pos = transform.localPosition;
		Vector3 futurePos = pos+ (new Vector3(0,0,Input.GetAxis("Mouse ScrollWheel")));
		if(futurePos.z < -1.25 && futurePos.z>-4) {
			transform.Translate (new Vector3(0,0,Input.GetAxis("Mouse ScrollWheel")));
		}


		head.Rotate (new Vector3(-180*Input.GetAxis ("Mouse Y") * Time.deltaTime,0,0));
		body.Rotate (new Vector3(0,180*Input.GetAxis ("Mouse X") * Time.deltaTime,0));


	}
}
