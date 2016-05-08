using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour {

	private Transform thisTransform = null;
	private Rigidbody2D thisBody = null;
	public float maxSpeed = 1f;
	public string HorzAxis = "Horizontal";

	void Start () {
		thisTransform = GetComponent<Transform>();
		thisBody = GetComponent<Rigidbody2D>();
	}
	

	void FixedUpdate () {
		float Horz = CrossPlatformInputManager.GetAxis ("Horizontal");
		thisBody.AddForce (Vector2.right * Horz * maxSpeed);

		thisBody.velocity = new Vector2 (Mathf.Clamp (thisBody.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp (thisBody.velocity.y, -maxSpeed, maxSpeed));
	}
}
