using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(MapController))]
public class LevelMapEditor : Editor
{
	int tempGridW, tempGridH;

	public MapController map;

	public void Awake ()
	{
		map = (MapController)target;
	}

	public void OnEnable ()
	{
		if (map.grid.objects == null || map.grid.objects.Length == 0) { 
			Debug.Log ("nuevo");
			map.grid.w = 5;
			map.grid.h = 5;
			map.grid.objects = new GameObject[map.grid.w * map.grid.h]; 
		}
		tempGridW = map.grid.w;
		tempGridH = map.grid.h;
		SceneView.onSceneGUIDelegate = GridUpdate;
		
	}

	public void OnDisable ()
	{
		SceneView.onSceneGUIDelegate -= GridUpdate;
	}

	public override void OnInspectorGUI ()
	{
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Grid W, H");
		tempGridW = EditorGUILayout.IntField (tempGridW, GUILayout.Width (50));
		tempGridH = EditorGUILayout.IntField (tempGridH, GUILayout.Width (50));

		if (GUILayout.Button ("Apply")) {
			GameObject[] tempGrid = new GameObject[tempGridW * tempGridH];

			for (int i = 0; i < tempGridW && i < map.grid.w; i++) {
				for (int j = 0; j < tempGridH && j < map.grid.h; j++) {
					tempGrid [j * tempGridW + i] = map.grid.getObject (i, j);
				}
			}

			for (int i = 0; i < map.grid.w; i++) {
				for (int j = 0; j < map.grid.h; j++) {
					map.grid.setObject (i, j, null);
				}
			}
			map.grid.objects = tempGrid;
			map.grid.w = tempGridW;
			map.grid.h = tempGridH;
			EditorUtility.SetDirty (map);
		}
		GUILayout.EndHorizontal ();

		if (GUILayout.Button ("Clear")) {
			for (int i = 0; i < map.grid.w; i++) {
				for (int j = 0; j < map.grid.h; j++) {
					if (map.grid.getObject (i, j)) {
						GameObject.DestroyImmediate (map.grid.getObject (i, j));
					}
					map.grid.setObject (i, j, null);
				}
			}
			map.grid.objects = new GameObject[map.grid.w * map.grid.h];
			EditorUtility.SetDirty (map);
		}

		DrawDefaultInspector ();
	}

	void GridUpdate (SceneView sceneview)
	{
		Event e = Event.current;
		Ray r = Camera.current.ScreenPointToRay (new Vector3 (e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
		Vector3 mousePos = r.origin;

 
		if (e.isKey && e.character == 'a') {
			GameObject obj;
			Object prefab = map.obstacles [Random.Range (0, map.obstacles.Length)];
         
			if (prefab) {
				Vector3 mapPos = map.transform.position;

				int objectGridX = Mathf.FloorToInt (((mousePos.x + mapPos.x - map.grid.x) + (map.obstacleW / 2)) / map.obstacleW);
				int objectGridY = Mathf.FloorToInt (((mapPos.y - mousePos.y + map.grid.y) + (map.obstacleH / 2)) / map.obstacleH);

				if (objectGridX >= 0 && objectGridX < map.grid.w && objectGridY >= 0 && objectGridY < map.grid.h) {
										
					obj = (GameObject)PrefabUtility.InstantiatePrefab (prefab);
					if (map.grid.getObject (objectGridX, objectGridY) != null) {
						GameObject.DestroyImmediate (map.grid.getObject (objectGridX, objectGridY));
					}
					map.grid.setObject (objectGridX, objectGridY, obj);

					obj.transform.parent = map.transform;

					float objectX = mapPos.x + map.grid.x + (objectGridX * map.obstacleW);
					float objectY = mapPos.y + map.grid.y - (objectGridY * map.obstacleH);

					obj.transform.position = new Vector3 (objectX, objectY, 0.0f);
					EditorUtility.SetDirty (map);
				}
			}
		}
	}
}
