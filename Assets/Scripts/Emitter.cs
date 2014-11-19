using UnityEngine;
using System.Collections;

public class Emitter : MonoBehaviour 
{
	// strage Wave prefab
	public GameObject[] waves;

	// now Wave
	private int currentWave;

	private Manager manager;

	IEnumerator Start () 
	{
		if (waves.Length == 0) {
			yield break;
		}

		// find Manager component
		manager = FindObjectOfType<Manager>();

		while(true) {

			// if not playing, wait
			while (manager.IsPlaying() == false) {
				yield return new WaitForEndOfFrame ();
			}

			// create Wave
			GameObject wave = (GameObject)Instantiate (waves [currentWave], transform.position, Quaternion.identity);
			wave.transform.parent = transform;

			while (wave.transform.childCount != 0) {
				yield return new WaitForEndOfFrame ();
			}

			Destroy (wave);

			if (waves.Length <= ++currentWave) {
				currentWave = 0;
			}
		}
	}
}
