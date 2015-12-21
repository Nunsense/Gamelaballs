using UnityEngine;
using System.Collections;

public class FollowBall : MonoBehaviour {
	public Transform sphere;
	private float height;
	public float behind;

	public float minHeight = -5;
	public float maxHeight = 0;

	Rigidbody ballBody;

	// Use this for initialization
	void Start () {
		height = minHeight;	
		ballBody = sphere.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		Vector3 ballPos = sphere.transform.position;
		Vector3 pos = transform.position;
		pos.y = ballPos.y + height;
		 // + new Vector3(0, height, -behind); 	

		transform.position = pos;

		height = Mathf.Lerp(height, Mathf.Clamp(minHeight + ballBody.velocity.y, minHeight, maxHeight), Time.deltaTime);

	}
}
