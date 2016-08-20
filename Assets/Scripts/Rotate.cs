using UnityEngine;

public class Rotate : MonoBehaviour 
{
	public float speed;

	void Update () 
	{
		this.transform.Rotate(new Vector3(0,speed,0));
	}
}
