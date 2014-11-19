using UnityEngine;

// 爆発を表現するモデル
public class Explosion : MonoBehaviour 
{
	void OnAnimationFinish ()
	{
		Destroy (this.gameObject);
	}
}
