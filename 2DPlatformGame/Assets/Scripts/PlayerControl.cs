using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour {

	public enum FACEDIRECTION
	{
		FACELEFT = -1, FACERIGHT = 1
	};
	public FACEDIRECTION facing = FACEDIRECTION.FACERIGHT;

	private Transform thisTransform = null;
	private Rigidbody2D thisBody = null;
	public float maxSpeed = 1f;
	public string HorzAxis = "Horizontal";
	public string JumpButton = "Jump";
	public float JumpPower = 600;

	void Start () {
		thisTransform = GetComponent<Transform>();
		thisBody = GetComponent<Rigidbody2D>();
	}
	

	void FixedUpdate () {
		float Horz = CrossPlatformInputManager.GetAxis ("Horizontal");
		thisBody.AddForce (Vector2.right * Horz * maxSpeed);
		if (CrossPlatformInputManager.GetButton(JumpButton)) {
			Jump ();
		}
		//clamp velocity
		thisBody.velocity = new Vector2 (Mathf.Clamp (thisBody.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp (thisBody.velocity.y, -maxSpeed, maxSpeed));

		//Flip direction
		if ((Horz < 0f && facing != FACEDIRECTION.FACELEFT) || (Horz > 0f && facing != FACEDIRECTION.FACERIGHT)) {
			FlipDirection ();
		}
	}

	private void FlipDirection(){
		facing = (FACEDIRECTION)((int)facing * -1f);
		Vector3 LocalScale = thisTransform.localScale;
		LocalScale.x *= -1f;
		thisTransform.localScale = LocalScale;
	}

	private void Jump(){
		thisBody.AddForce (Vector2.up * JumpPower);
	}
}
