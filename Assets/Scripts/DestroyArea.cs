using UnityEngine;

// ゲーム範囲を超えたら自動でインスタンスを削除するスクリプト
public class DestroyArea : MonoBehaviour 
{
	void OnTriggerExit2D (Collider2D collider)
	{
		Destroy (collider.gameObject);
	}
}
