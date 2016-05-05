using UnityEngine;
using System.Collections;

public class PingPongMotion : MonoBehaviour {

	private Transform ThisTransform = null;

	private Vector3 OriPos = Vector3.zero;

	public Vector3 MoveAxes = Vector2.zero;

	public float Distance = 3f;

	// Use this for initialization
	void Awake () {
		ThisTransform = GetComponent<Transform> ();

		OriPos = ThisTransform.position;
	}
	
	// Update is called once per frame
	void Update () {
		ThisTransform.position = OriPos + MoveAxes * Mathf.PingPong (Time.time, Distance);
	}
}
