using UnityEngine;
using System.Collections;

[System.Serializable]
public class Grid
{
	public int w;
	public int h;

	public float x = -9f;
	public float y = 8;

	public GameObject[] objects;

	public void setObject(int x, int y, GameObject obj) {
		objects[y * w + x] = obj;
	}

	public GameObject getObject(int x, int y) {
		return objects[y * w + x];
	}
}