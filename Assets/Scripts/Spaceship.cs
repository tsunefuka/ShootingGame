using UnityEngine;

// to require Rigitbody2D component
[RequireComponent(typeof(Rigidbody2D))]
public class Spaceship : MonoBehaviour 
{
	// movement speed
	public float speed;

	// interval shoot bullet
	public float shotDelay;

	// prefav of bullet
	public GameObject bullet;

	// flag to shot bullet
	public bool canShot;

	// GameObject of explosion
	public GameObject explosion;

	public void Explosion()
	{
		Instantiate (explosion, transform.position, transform.rotation);
	}

	// create bullet
	public void Shot (Transform origin)
	{
		Instantiate (bullet, origin.position, origin.rotation);
	}
}
