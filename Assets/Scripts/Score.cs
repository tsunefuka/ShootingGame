using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour 
{
	// スコアを表示するGUIText
	public GUIText scoreGUIText;

	// ハイスコアを表示するGUIText
	public GUIText highScoreGUIText;

	private int score;
	private int highScore;

	// PlayerPrefsで保存するためのキー
	private string highScoreKey = "highScore";

	void Start () {
		this.Initialize ();
	}
	
	void Update () {
		this.scoreGUIText.text = this.score.ToString ();
		this.highScoreGUIText.text = "HighScore : " + this.highScore.ToString ();
	}

	private void Initialize ()
	{
		this.score = 0;
		this.highScore = PlayerPrefs.GetInt (this.highScoreKey, 0);
	}

	public void AddPoint (int point)
	{
		this.score += point;
	}

	public void Save ()
	{
		// ハイスコアをPlayerPrefsに保存
		PlayerPrefs.SetInt (this.highScoreKey, highScore);
		PlayerPrefs.Save ();

		Initialize ();
	}
}
