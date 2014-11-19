using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	// ヒットポイント
	public int hp = 1;

	// Spaceshipコンポーネント
	Spaceship spaceship;

	IEnumerator Start () 
	{
		// 移動処理
		spaceship = GetComponent<Spaceship> ();
		Move (transform.up * -1);

		// if canShot is false, finish corutine here
		if (spaceship.canShot == false) {
			yield break;
		}

		while (true) {
			// 射撃処理
			for (int i = 0; i < transform.childCount; i++) {
				Transform shotPosition = transform.GetChild(i);
				spaceship.Shot (shotPosition);
			}

			yield return new WaitForSeconds (spaceship.shotDelay);
		}
	}

	public void Move (Vector2 direction)
	{
		rigidbody2D.velocity = direction * spaceship.speed;
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		string layerName = LayerMask.LayerToName (collider.gameObject.layer);

		if (layerName != "Bullet(Player)") {
			return;
		}

		Destroy (collider.gameObject);
		spaceship.Explosion ();
		Destroy (gameObject);
	}
}
