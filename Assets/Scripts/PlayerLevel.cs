using UnityEngine;

// 自機がレベルごとに変わるパラメータを内包するクラス
public class PlayerLevel : MonoBehaviour
{
    // 弾
	public GameObject bullet;

	// 発射間隔(sec)
	public float interval_shot;

	// 弾の発射位置のグループ名
	public string shot_position_name;
}
