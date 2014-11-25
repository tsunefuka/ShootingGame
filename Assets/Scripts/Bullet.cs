using UnityEngine;
using System.Collections;

// 弾モデル
public class Bullet : MonoBehaviour 
{
	// 弾の移動スピード
	public float speed = 10;

	// ゲームオブジェクト生成から削除するまでの時間
	public float lifeTime = 5;

	// 攻撃力
	public int power = 1;

	void Start () 
	{
		// 弾の移動処理
		this.rigidbody2D.velocity = this.transform.up.normalized * this.speed;

		// 一定時間たったら削除
		Destroy (this.gameObject, this.lifeTime);
	}
}
