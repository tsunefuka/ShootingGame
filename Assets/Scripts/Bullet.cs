using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	// 弾の移動スピード
	public int speed = 10;

	// ゲームオブジェクト生成から削除するまでの時間
	public float lifeTime = 5;

	// 攻撃力
	public int power = 1;

	void Start () {
		rigidbody2D.velocity = transform.up.normalized * speed;

		Destroy (gameObject, lifeTime);
	}
}
