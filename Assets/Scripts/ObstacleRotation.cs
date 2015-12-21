using UnityEngine;
using System.Collections;

public class ObstacleRotation : MonoBehaviour
{
	static float[] rotations = { 0, 90, 180, 270 };

	// Use this for initialization
	void Start ()
	{
		Vector3 euler = transform.eulerAngles;
		euler.z = rotations[Random.Range (0, rotations.Length)];
		transform.eulerAngles = euler;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
