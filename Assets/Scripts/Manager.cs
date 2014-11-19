using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour 
{
	public GameObject player;

	private GameObject title;

	void Awake () 
	{
		title = GameObject.Find ("Title");
	}
	
	void Update () 
	{
		if (IsPlaying () == false && Input.GetKeyDown (KeyCode.X))
		{
			GameStart ();
		}
	}

	void GameStart ()
	{
		// create player by active diactive when game started
		title.SetActive (false);
		Instantiate (player, player.transform.position, player.transform.rotation);
	}

	public void GameOver ()
	{
		// display title if game over
		title.SetActive (true);
	}

	public bool IsPlaying ()
	{
		// judge IsPlaying by title is active/nonactive
		return title.activeSelf == false;
	}
}
