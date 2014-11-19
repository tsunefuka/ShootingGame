using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	// Spaceshipコンポーネント
	Spaceship spaceship;

	// call Start method as coroutine
	IEnumerator Start ()
	{
		spaceship = GetComponent<Spaceship> ();

		while (true) {	
			// shot bullet same position and rotation to player
			spaceship.Shot (transform);

			// play shoot sound
			audio.Play ();

			// sleep 0.05 sec
			yield return new WaitForSeconds (0.05f);
		}
	}

	void Update () 
	{
	    float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");

		Vector2 direction = new Vector2 (x, y).normalized;
		Move (direction);
	}

	void Move (Vector2 direction)
	{
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		
		Vector2 pos = transform.position;
		pos += direction * spaceship.speed * Time.deltaTime;

		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);
		
		transform.position = pos;
	}

	// called hitted to collider
	void OnTriggerEnter2D (Collider2D collider)
	{
		// get Layer name
		string layerName = LayerMask.LayerToName(collider.gameObject.layer);

		if ( layerName == "Bullet(Enemy)") {
			Destroy (collider.gameObject);
		}

		if (layerName == "Bullet(Enemy)" || layerName == "Enemy") {
			// find Manager component, and call GameOver
			FindObjectOfType<Manager>().GameOver();

			spaceship.Explosion();
			Destroy (gameObject);
		}
	}
}
