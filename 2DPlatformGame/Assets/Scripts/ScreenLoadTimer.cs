using UnityEngine;
using System.Collections;

public class ScreenLoadTimer : MonoBehaviour {

	public int SceneID = 0;
	public float TimeDelay = 5f;

	void Start () {
		Invoke ("LoadScene", TimeDelay);
	}

	void LoadScene(){
		Application.LoadLevel (SceneID);
	}
}
