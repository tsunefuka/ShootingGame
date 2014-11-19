using UnityEngine;
using System.Collections;

// プレイヤーの自機を表現するモデル
public class Player : MonoBehaviour 
{
	// Spaceshipコンポーネント
	Spaceship spaceship;

	IEnumerator Start ()
	{
		this.spaceship = GetComponent<Spaceship> ();

		while (true) {	
			// 射撃処理
			this.spaceship.Shot (transform);

			audio.Play ();

			yield return new WaitForSeconds (0.05f);
		}
	}

	void Update () 
	{
		// プレイヤーの入力処理
	    float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");

		Vector2 direction = new Vector2 (x, y).normalized;
		this.Move (direction);
	}

	void Move (Vector2 direction)
	{
		// 自機の移動処理
		// 画面端の向こうには行けなくする
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		
		Vector2 pos = this.transform.position;
		pos += direction * this.spaceship.speed * Time.deltaTime;

		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);
		
		this.transform.position = pos;
	}

	// 衝突処理
	void OnTriggerEnter2D (Collider2D collider)
	{
		string layerName = LayerMask.LayerToName(collider.gameObject.layer);

		// 敵の弾に当たったら敵の弾を削除
		if (layerName == "Bullet(Enemy)") {
			Destroy (collider.gameObject);
		}

		// 敵の弾か敵機い当たったら自分を削除
		if (layerName == "Bullet(Enemy)" || layerName == "Enemy") {
			// find Manager component, and call GameOver
			FindObjectOfType<Manager>().GameOver();

			this.spaceship.Explosion();
			Destroy (this.gameObject);
		}
	}
}
