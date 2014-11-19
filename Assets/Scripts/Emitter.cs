using UnityEngine;
using System.Collections;

// 敵の出現フォーメーションを管理するクラス
public class Emitter : MonoBehaviour 
{
	public GameObject[] waves;

	private int currentWave;

	private Manager manager;

	IEnumerator Start () 
	{
		if (this.waves.Length == 0) 
		{
			yield break;
		}

		// マネージャーオブジェクト取得
		this.manager = FindObjectOfType<Manager>();

		while(true) 
		{
			// ゲームが始まるまで何もしない
			while (this.manager.IsPlaying() == false) 
			{
				yield return new WaitForEndOfFrame ();
			}

			// 敵の出現フォーメーションを生成
			GameObject wave = (GameObject)Instantiate (this.waves [this.currentWave], this.transform.position, Quaternion.identity);
			wave.transform.parent = this.transform;

			// 全員死ぬまで待機
			while (wave.transform.childCount != 0) 
			{
				yield return new WaitForEndOfFrame ();
			}

			// 敵が全員死んだらフォーメーションも削除
			Destroy (wave);

			// ひと通りのフォーメーションが終わったら最初に戻る
			if (this.waves.Length <= ++this.currentWave) 
			{
				this.currentWave = 0;
			}
		}
	}
}
