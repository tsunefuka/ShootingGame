using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour 
{
	// speed scroll
	public float speed = 0.1f;

	void Update () 
	{
		// create offset 
		float y = Mathf.Repeat (Time.time * this.speed, 1);
		Vector2 offset = new Vector2 (0, y);

		// set offset to material
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}
}
