using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Spaceship : MonoBehaviour 
{
	// 移動速度
	public float speed;

	// 射撃間隔
	public float shotDelay;

	// 弾のプレハブ
	public GameObject bullet;

	// 射撃を行うか
	public bool canShot;

	// 爆発のプレハブ
	public GameObject explosion;

	// アニメーターコンポーネント
	private Animator animator;

	protected void Initialize ()
	{
		this.animator = GetComponent<Animator> ();
	}

	public void Explosion()
	{
		Instantiate (this.explosion, this.transform.position, this.transform.rotation);
	}

	public void Shot (Transform origin)
	{
		Instantiate (this.bullet, origin.position, origin.rotation);
	}

	public Animator GetAnimator ()
	{
		return this.animator;
	}

	protected abstract void Move (Vector2 direction);
}
