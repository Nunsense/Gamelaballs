using UnityEngine;
using System.Collections;

[System.Serializable]
public class MapController : MonoBehaviour
{
	public GameObject[] obstacles;
	public float obstacleW;
	public float obstacleH;

	public Grid grid;

	// Use this for initialization
	void Start ()
	{
//		Rigidbody body = GetComponent<Rigidbody> ();
//		for (int i = 0; i < gridH; i++) {
//			for (int j = 0; j < gridW; j++) {
//				GameObject newObstacle = GameObject.Instantiate (obstacles [Random.Range (0, obstacles.Length)]);
//				newObstacle.transform.parent = transform;
//				newObstacle.transform.position = new Vector3 (gridX + j * obstacleW, gridY - i * obstacleH, -0.5f);
//				ConfigurableJoint joint = newObstacle.GetComponent<ConfigurableJoint> ();
//				if (joint != null) {
//					joint.connectedBody = body;
//				}
//			}
//		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnDrawGizmos ()
	{
		Vector3 pos = transform.position;

		float initY = pos.y + grid.y;
		float totalH = -(((grid.h - 1) * obstacleH) - initY);

		float initX = pos.x + grid.x;
		float totalW = initX + ((grid.w - 1) * obstacleW);

		for (float x = initX; x <= totalW + 1; x += obstacleW) {
			Gizmos.DrawLine (new Vector3 (x, initY, 0.0f), new Vector3 (x, totalH, 0.0f));
		}
		for (float y = initY; y > totalH; y -= obstacleH) {
			Gizmos.DrawLine (new Vector3 (initX, y, 0.0f), new Vector3 (totalW, y, 0.0f));
		}
	    	
	}
}
