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
	public float JumpPower = 60;
	private bool CanJump = true;
	public float JumpTimeOut = 1f;

	public CircleCollider2D FeetCillider = null;
	public bool isGround = false;
	public LayerMask GroundLayer;

	void Awake () {
		thisTransform = GetComponent<Transform>();
		thisBody = GetComponent<Rigidbody2D>();
	}
	
	private bool GetGrounded(){
		//Check ground
		Vector2 CircleCenter = new Vector2(thisTransform.position.x, thisTransform.position.y) + FeetCillider.offset;
		Collider2D[] HitColliders = Physics2D.OverlapCircleAll (CircleCenter, FeetCillider.radius, GroundLayer);
		if (HitColliders.Length > 0) {
			return true;
		}

		return false;
	}

	void FixedUpdate () {

		isGround = GetGrounded ();
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

		if (!isGround || !CanJump)return;

		thisBody.AddForce (Vector2.up * JumpPower);
		CanJump = false;
		Invoke ("ActivateJump", JumpTimeOut);
	}

	private void ActivateJump(){
		CanJump = true;
	}
}
