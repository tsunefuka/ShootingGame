using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour 
{
	public float speed = 0.5f;

	public int powerup = 1;

	void Start ()
	{
		// 移動処理
		this.Move (this.transform.up * -1);
	}

	protected void Move (Vector2 direction)
	{
		this.rigidbody2D.velocity = direction * this.speed;
	}
}
