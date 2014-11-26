using UnityEngine;
using System.Collections;

// プレイヤーの自機を表現するモデル
public class Player : Spaceship 
{
	// 自機のレベル
	// レベル上げても自動で強くなる訳ではないので、Inspectorから変更させない
	private int level = 1;

	// 自機のレベルごとのパラメータ
	public GameObject[] levels;

	IEnumerator Start ()
	{
		base.Initialize ();

		while (true) {	
			// 射撃処理
			this.Shot ();

			audio.Play ();

			yield return new WaitForSeconds (this.getPlayerLevel().interval_shot);
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

	public void Shot ()
	{
		// レベルにあった射撃位置から射撃する
		PlayerLevel level = this.getPlayerLevel ();
		Transform shot_positions_transform = this.transform.Find (level.shot_position_name);
		for (int i = 0; i < shot_positions_transform.childCount; i++)
		{
			Transform shotPosition = shot_positions_transform.GetChild(i);
			base.Shot (shotPosition);
		}
	}

	protected override void Move (Vector2 direction)
	{
		float speed = this.speed;

		// Shiftキーを押してる間はゆっくり動く
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
		{
			speed *= 0.1f;
		}

		// 自機の移動処理
		// 画面端の向こうには行けなくする
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		
		Vector2 pos = this.transform.position;
		pos += direction * speed * Time.deltaTime;

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

		// 敵の弾か敵機に当たったら自分を削除
		if (layerName == "Bullet(Enemy)" || layerName == "Enemy") {
			// find Manager component, and call GameOver
			FindObjectOfType<Manager>().GameOver();

			this.Explosion();
			Destroy (this.gameObject);
		}

		// アイテム回収処理
		if (layerName == "Item")
		{
			this.EffectItem (collider.transform.GetComponent<Item> ());
			Destroy (collider.gameObject);
		}
	}

	// アイテムの効果発動
	void EffectItem (Item item)
	{
        // 本来はアイテムの種類によって処理を切り分ける。今はレベルアップのみ実装。
		this.LevelUp ();
	}

	// レベルアップ処理
	void LevelUp ()
	{
		if (this.level < this.levels.Length) 
		{
			this.level++;
			this.bullet = this.getPlayerLevel ().bullet;
		}
	}

	// 現在のレベルに対応したPlayerLevelを取得する
	protected PlayerLevel getPlayerLevel ()
	{
		return this.levels [this.level-1].transform.GetComponent<PlayerLevel>();
	}
}
