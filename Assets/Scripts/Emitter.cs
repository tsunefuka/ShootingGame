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

		// find Manager component
		this.manager = FindObjectOfType<Manager>();

		while(true) 
		{
			// if not playing, wait
			while (this.manager.IsPlaying() == false) 
			{
				yield return new WaitForEndOfFrame ();
			}

			// create Wave
			GameObject wave = (GameObject)Instantiate (this.waves [this.currentWave], this.transform.position, Quaternion.identity);
			wave.transform.parent = this.transform;

			while (wave.transform.childCount != 0) 
			{
				yield return new WaitForEndOfFrame ();
			}

			Destroy (wave);

			if (this.waves.Length <= ++this.currentWave) 
			{
				this.currentWave = 0;
			}
		}
	}
}
