using UnityEngine;
using System.Collections;

// 敵を表現するモデル
public class Enemy : Spaceship
{
	// ヒットポイント
	public int hp = 1;

	// 撃破時の獲得ポイント
	public int point = 100;

	// 撃破時のドロップアイテム
	public GameObject drop_item;

	IEnumerator Start ()
	{
		base.Initialize ();

		// 移動処理
		this.Move (this.transform.up * -1);

		// if canShot is false, finish corutine here
		if (this.canShot == false)
		{
			yield break;
		}

		while (true)
		{
			// 射撃処理
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform shotPosition = transform.GetChild (i);
				this.Shot (shotPosition);
			}

			yield return new WaitForSeconds (this.shotDelay);
		}
	}

	protected override void Move (Vector2 direction)
	{
		this.rigidbody2D.velocity = direction * this.speed;
	}

	// 衝突処理
	void OnTriggerEnter2D (Collider2D collider)
	{
		// 自機の弾としか衝突処理しない
		string layerName = LayerMask.LayerToName (collider.gameObject.layer);
		if (layerName != "Bullet(Player)") 
		{
			return;
		}

		// 弾衝突処理
		Bullet bullet = collider.transform.GetComponent<Bullet> ();
		this.hp -= bullet.power;
		Destroy (collider.gameObject);

		if (this.hp <= 0) 
		{
			// 死亡処理
			FindObjectOfType<Score> ().AddPoint (point);
			this.Explosion ();
			Destroy (this.gameObject);
			this.DropItem (collider);
		}
		else
		{
			this.GetAnimator ().SetTrigger ("Damage");
		}
	}

	// 撃破時のドロップ
	private void DropItem (Collider2D collider)
	{
		if (this.drop_item != null)
		{
			Instantiate (this.drop_item, this.transform.position, this.transform.rotation);
		}
	}
}
