using UnityEngine;
using System.Collections;

public class MouseRaycastPlane : MonoBehaviour {

	public GameObject Floor;
	private Plane plane;
	
	void Start()
	{
		plane = new Plane(Vector3.up, Floor.transform.position);
	}
	
	void LateUpdate()
	{
		if (Input.GetMouseButton(0))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			var rayDistance = 0f;
			if (plane.Raycast(ray, out rayDistance))
			{
				Debug.Log("Plane Raycast hit at distance: " + rayDistance);
				var hitPoint = ray.GetPoint(rayDistance);
				
				var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
				go.transform.position = hitPoint + new Vector3(0, go.transform.localScale.y / 2, 0);
				Debug.DrawRay (ray.origin, ray.direction * rayDistance, Color.green);
			}
			else
				Debug.DrawRay (ray.origin, ray.direction * 10, Color.red);
			
		}
	}


}
