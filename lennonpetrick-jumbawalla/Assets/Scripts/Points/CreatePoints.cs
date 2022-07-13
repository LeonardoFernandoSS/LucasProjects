using UnityEngine;
using System.Collections;

public class CreatePoints : MonoBehaviour
{
	public GameObject prefabsBlobsPoints;
	public Transform positionBlobsPoints;
	public Vector2 valuePoints;
	public int amountPoints;

	public void create()
	{
		int _points = (int)Random.Range(valuePoints.x, valuePoints.y);
		ControlPoints.createPrefabPoints( _points , positionBlobsPoints.position, amountPoints, prefabsBlobsPoints);
	}
}
