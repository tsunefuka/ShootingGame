using UnityEngine;
using System.Collections;

// 背景画像モデル
public class Background : MonoBehaviour 
{
	public float scrollSpeed = 0.1f;

	void Update () 
	{
		// 背景画像の自動スクロール
		float y = Mathf.Repeat (Time.time * this.scrollSpeed, 1);
		Vector2 offset = new Vector2 (0, y);
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}
}
