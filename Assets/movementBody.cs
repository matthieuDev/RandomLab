using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementBody : MonoBehaviour {
	Rigidbody MyRigidbody;
	// Use this for initialization
	void Start () {
		
		MyRigidbody = GetComponents<Rigidbody>()[0];	
	}


	
	// Update is called once per frame
	void Update () {
		print (MyRigidbody.velocity);
		MyRigidbody.velocity = Vector3.zero;
		Vector3 pos = transform.position;
		Vector3 vitesse = Vector3.zero;
		if (Input.GetKey("w")) {
			vitesse.z = Time.deltaTime * 2.5f;
		}
		if (Input.GetKey("s")) {
			vitesse.z =- Time.deltaTime * 2.5f;
			//transform.Translate(new Vector3(0,0,-Time.deltaTime * 2.5f));
		}
		if (Input.GetKey("a")) {
			vitesse.x =- Time.deltaTime * 2.5f;
			//transform.Translate(new Vector3(-Time.deltaTime * 2.5f,0,0));
		}
		if (Input.GetKey("d")) {
			vitesse.x = Time.deltaTime * 2.5f;
			//transform.Translate(new Vector3(Time.deltaTime * 2.5f,0,0));
		}
		transform.Translate (vitesse);
	}
}
