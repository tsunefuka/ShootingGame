using UnityEngine;
using System.Collections;

// ゲームを管理するクラス
public class Manager : MonoBehaviour 
{
	public GameObject player;

	private GameObject title;

	void Awake () 
	{
		this.title = GameObject.Find ("Title");
	}
	
	void Update () 
	{
		if (this.IsPlaying () == false && Input.GetKeyDown (KeyCode.X))
		{
			this.GameStart ();
		}
	}

	void GameStart ()
	{
		// create player by active diactive when game started
		this.title.SetActive (false);
		Instantiate (this.player, this.player.transform.position, this.player.transform.rotation);
	}

	public void GameOver ()
	{
		// ハイスコアの保存＋獲得スコアのリセット
		FindObjectOfType<Score> ().Save ();

		// display title if game over
		this.title.SetActive (true);
	}

	public bool IsPlaying ()
	{
		// judge IsPlaying by title is active/nonactive
		return this.title.activeSelf == false;
	}
}
