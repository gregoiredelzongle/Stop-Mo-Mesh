using UnityEngine;
using System.Collections;

public class OrbitAroundPoint : MonoBehaviour {

	public Transform pivot;
	public float speed = 1.0f;
	
	// Update is called once per frame
	void Update () {
		if (pivot == null)
			return;

		transform.RotateAround (pivot.position, Vector3.up, speed * Time.deltaTime);
	}
}
