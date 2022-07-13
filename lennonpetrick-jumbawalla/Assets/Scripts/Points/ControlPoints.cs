using UnityEngine;
using System.Collections;

public class ControlPoints : MonoBehaviour
{
	public static void createPrefabPoints( int valuePoints, Vector3 positionPoints, int amountPoint, GameObject prefabPoints )
	{
		GameObject _points;

		for(int i = 0; i < amountPoint; i++) 
		{
			_points = Instantiate( prefabPoints, new Vector3(positionPoints.x + Random.Range(-6, 6), positionPoints.y, positionPoints.z), Quaternion.identity) as GameObject;

			_points.GetComponent<PointBehaviour>().setPoints( valuePoints );
		}
	}
}
